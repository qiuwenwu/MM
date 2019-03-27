'use strict';
//定义一个模块，传入require对象。并执行函数。

define(['vue', 'vueRouter', 'mmVue'], function (vue, vueRouter, $) {
    vueRouter.prototype.goBack = function () {
        this.isBack = true;
        window.history.go(-1);
    };

    vue.use(vueRouter); //引用全局组件——路由器
    var routePath = "/admin";
    var filePath = "/admin";

    ///设置多个路由路径
    var arr = [{0}];

    //console.log(arr.length); //判断路由个数
    var routes = $.forRoute(routePath, filePath, arr, true); //配置路由路径

    //添加路由
    routes.push({
        path: routePath + '/login',
        component: function component(resolve) {
            return require(['./pages/login'], resolve);
        }
    });

    routes.push({
        path: "/",
        redirect: routePath + '/index'
    });

    var router = new vueRouter($.newRouter(routes, true)); //生成路由器

    //注册全局钩子用来拦截导航
    router.beforeEach(function (to, from, next) {
        $.validateUser(to, from, next, routePath + '/login'); //验证用户
    });
    return router;
});