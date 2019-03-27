'use strict';
//配置加载的js模块

require.config({
    baseUrl: '/app/admin/',
    preserveLicenseComments: false, //是否保留注释
    waitSeconds: 0,
    paths: {
        css: '/static/js/css.min',
        text: '/static/js/text.min',
        zepto: '/static/js/zepto.min',
        vue: '/static/js/vue', //生产版||调试版
        vuex: '/static/js/vuex.min',
        vueRouter: '/static/js/vue-router.min',
        mmSdk: '/static/js/mm-sdk',
        mmUI: '/static/js/mm-ui',
        mmVue: '/static/js/mm-vue',
        store: './store',
        router: './router',
		app: './pages/app'
    },
    shim: {
        zepto: {
            exports: 'Zepto'
        },
        vue: {
            exports: 'Vue'
        },
        vuex: {
            deps: ['vue']
        },
        vueRouter: {
            deps: ['vue']
        },
        mmSdk: {
            deps: ['zepto']
        }
    }
});

///初始化主程序
///vue：vue模块
///store：状态管理器
///router：路由管理器
///app：初始应用
require(['vue', 'mmSdk', 'mmUI', 'store', 'router', 'app'], function (vue, mmSdk, mmUI, store, router, app) {
    vue.config.debug = true; //开启调试模式
    vue.config.devtools = true; //开启开发者工具

    //mmUI.setLoadItem(['page']);
    vue.use(mmUI); //注册mmUI插件（用于注册全局组件）

    var vm = new vue({
		render: h => h(app),
        el: '#app', //指定作用域
        store: store, //引用缓存管理器
        router: router //引用路由管理器
	});
});
