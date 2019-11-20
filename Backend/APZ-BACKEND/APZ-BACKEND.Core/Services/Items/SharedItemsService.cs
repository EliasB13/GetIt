﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.SharedItems;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Interfaces;
using APZ_BACKEND.Core.Mappers;
using APZ_BACKEND.Core.Services.Communication;

namespace APZ_BACKEND.Core.Services.Items
{
	public class SharedItemsService : ISharedItemsService
	{
		private readonly IAsyncRepository<SharedItem> sharedItemsRepository;
		private readonly IAsyncRepository<BusinessUser> businessUsersRepository;
		private readonly IAsyncRepository<ItemTaking> itemTakingsRepository;
		private readonly IAsyncRepository<ItemTakingLine> itemTakingLinesRepository;
		private readonly IAsyncRepository<EmployeeRoleItem> employeesRoleItemsRepository;
		private readonly IAsyncRepository<EmployeesRole> employeesRolesRepository;

		public SharedItemsService(IAsyncRepository<SharedItem> sharedItemsRepository,
			IAsyncRepository<BusinessUser> businessUsersRepository,
			IAsyncRepository<ItemTaking> itemTakingsRepository,
			IAsyncRepository<ItemTakingLine> itemTakingLinesRepository,
			IAsyncRepository<EmployeeRoleItem> employeesRoleItemsRepository,
			IAsyncRepository<EmployeesRole> employeesRolesRepository)
		{
			this.sharedItemsRepository = sharedItemsRepository;
			this.businessUsersRepository = businessUsersRepository;
			this.itemTakingsRepository = itemTakingsRepository;
			this.itemTakingLinesRepository = itemTakingLinesRepository;
			this.employeesRoleItemsRepository = employeesRoleItemsRepository;
			this.employeesRolesRepository = employeesRolesRepository;
		}

		public async Task<GenericServiceResponse<SharedItem>> AddItemToBusiness(int businessUserId, AddSharedItemRequest addItemDto)
		{
			try
			{
				var businessUser = await businessUsersRepository.GetByIdAsync(businessUserId);
				if (businessUser == null)
					return new GenericServiceResponse<SharedItem>($"Business user with id: {businessUserId} wasn't found");

				var item = addItemDto.ToSharedItem(businessUser);

				await sharedItemsRepository.AddAsync(item);
				return new GenericServiceResponse<SharedItem>(item);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<SharedItem>("Error | Adding item to business: " + ex.Message);
			}
		}

		public async Task<GenericServiceResponse<SharedItem>> AddItemToEmployeesRole(int businessUserId, int itemId, int roleId)
		{
			try
			{
				var businessUser = await businessUsersRepository.GetByIdAsync(businessUserId);
				if (businessUser == null)
					return new GenericServiceResponse<SharedItem>($"Business user with id: {businessUserId} wasn't found");

				var item = await sharedItemsRepository.SingleOrDefaultAsync(si => si.Id == itemId, si => si.BusinessUser);
				if (item == null)
					return new GenericServiceResponse<SharedItem>($"Shared item with id: {itemId} wasn't found");

				var role = await employeesRolesRepository.GetByIdAsync(roleId);
				if (role == null)
					return new GenericServiceResponse<SharedItem>($"Role with id: {roleId} wasn't found");

				if (item.BusinessUser.Id != businessUserId)
					return new GenericServiceResponse<SharedItem>("Item doesn't belong to business");

				var isItemInRole = await employeesRoleItemsRepository.AnyAsync(eri => eri.EmployeesRole.Id == roleId && eri.SharedItem.Id == itemId);
				if (isItemInRole)
					return new GenericServiceResponse<SharedItem>($"Item with id: {itemId} already exists in role with id: {roleId}");

				var employeeRoleItem = new EmployeeRoleItem
				{
					EmployeesRole = role,
					SharedItem = item
				};

				await employeesRoleItemsRepository.AddAsync(employeeRoleItem);

				return new GenericServiceResponse<SharedItem>(item);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<SharedItem>("Error | Adding item to role: " + ex.Message);
			}
		}

		public async Task<IEnumerable<SharedItemDto>> GetBusinessItems(int businessUserId)
		{
			var items = await sharedItemsRepository.ListAllAsync(si => si.BusinessUser.Id == businessUserId, si => si.BusinessUser);
			var itemTakingLines = await itemTakingLinesRepository.ListAllAsync();
			if (items.Count() > 0)
			{
				var itemDtos = items.Select(i =>
				{
					var isItemTaken = itemTakingLines.Any(itl => itl.SharedItemId == i.Id && itl.IsTaken);
					return i.ToDto(isItemTaken);
				}).ToList();

				return itemDtos;
			}

			return new List<SharedItemDto>();
		}

		public async Task<IEnumerable<SharedRoleItemDto>> GetEmployeesRoleItems(int roleId, int businessUserId)
		{
			var roleItems = await employeesRoleItemsRepository.ListAllAsync(eri => eri.EmployeesRoleId == roleId, eri => eri.SharedItem);
			var itemTakingLines = await itemTakingLinesRepository.ListAllAsync();
			if (roleItems.Count() > 0)
			{
				var itemsDto = roleItems.Select(ri =>
				{
					var isItemTaken = itemTakingLines.Any(itl => itl.SharedItemId == ri.Id && itl.IsTaken);
					return ri.SharedItem.ToDto(isItemTaken, ri.Id, roleId);
				});

				return itemsDto;
			}
			return new List<SharedRoleItemDto>();
		}

		public async Task<GenericServiceResponse<SharedItemDto>> GetItem(int businessUserId, int itemId)
		{
			var businessUser = await businessUsersRepository.GetByIdAsync(businessUserId);
			if (businessUser == null)
				return new GenericServiceResponse<SharedItemDto>($"Business user with id: {businessUserId} wasn't found");

			var item = await sharedItemsRepository.GetByIdAsync(itemId);
			if (item == null)
				return new GenericServiceResponse<SharedItemDto>($"Item with id: {itemId} wasn't found");

			var isTaken = await itemTakingLinesRepository.AnyAsync(itl => itl.SharedItemId == item.Id && itl.IsTaken);
			var itemDto = item.ToDto(isTaken);

			return new GenericServiceResponse<SharedItemDto>(itemDto);

		}

		public async Task<GenericServiceResponse<SharedItem>> Update(UpdateSharedItemRequest dto, int id, int businessUserId)
		{
			try
			{
				var item = await sharedItemsRepository.SingleOrDefaultAsync(si => si.Id == id, si => si.BusinessUser);
				if (item == null)
					return new GenericServiceResponse<SharedItem>($"Shared item with id: {id} wasn't found");
				
				if (item.BusinessUser.Id != businessUserId)
					return new GenericServiceResponse<SharedItem>("Item doesn't belong to business");

				item.UpdateSharedItemFromDto(dto);
				await sharedItemsRepository.UpdateAsync(item);

				return new GenericServiceResponse<SharedItem>(item);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<SharedItem>("Error | Updating shared item: " + ex.Message);
			}
		}

		public async Task<GenericServiceResponse<SharedItem>> Delete(int itemId, int businessUserId)
		{
			try
			{
				var item = await sharedItemsRepository.SingleOrDefaultAsync(si => si.Id == itemId, si => si.BusinessUser);
				if (item == null)
					return new GenericServiceResponse<SharedItem>($"Shared item with id: {itemId} wasn't found");

				if (item.BusinessUser.Id != businessUserId)
					return new GenericServiceResponse<SharedItem>("Item doesn't belong to business");

				await sharedItemsRepository.DeleteAsync(item);
				return new GenericServiceResponse<SharedItem>(item);
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<SharedItem>("Error | Deleting shared item: " + ex.Message);
			}
		}

		public async Task<GenericServiceResponse<EmployeeRoleItem>> RemoveItemFromEmployeesRole(int roleItemId, int businessUserId)
		{
			try
			{
				var roleItem = await employeesRoleItemsRepository.SingleOrDefaultAsync(ri => ri.Id == roleItemId);
				if (roleItem == null)
					return new GenericServiceResponse<EmployeeRoleItem>($"Role item with id: {roleItemId} wasn't found");

				var item = await sharedItemsRepository.SingleOrDefaultAsync(si => si.Id == roleItem.SharedItemId, si => si.BusinessUser);
				if (item == null)
					return new GenericServiceResponse<EmployeeRoleItem>($"Shared item with id: {roleItem.SharedItemId} wasn't found");

				if (item.BusinessUser.Id != businessUserId)
					return new GenericServiceResponse<EmployeeRoleItem>("Item doesn't belong to business");

				await employeesRoleItemsRepository.DeleteAsync(roleItem);
				return new GenericServiceResponse<EmployeeRoleItem>(roleItem);

			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<EmployeeRoleItem>("Error | Removing item from employees role: " + ex.Message);
			}
		}
	}
}
