import Vue from "vue";
import Vuex from "vuex";

import { alert } from "./modules/alert.module";
import { account } from "./modules/account.module";
import { users } from "./modules/users.module";
import { businessItems } from "./modules/business-items.module";

Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    alert,
    account,
    users,
    businessItems
  }
});
