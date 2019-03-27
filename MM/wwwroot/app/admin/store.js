'use strict';

define(['vue', 'vuex', 'mmVue'], function (vue, vuex, $) {
    vue.use(vuex); //引用vuex插件
    var store = $.newCache(); //新建缓存模型

    //创建缓存
    $.addCache(store, 'nav', $.cacheModel("/apis/web/nav?page=0", "title", "fid", 'title'));
    $.addCache(store, 'group', $.cacheModel("/apis/user/group?page=0&field=*", "gid", 'name'));
    $.addCache(store, 'format', $.cacheModel("/apis/number/format?page=0&field=`fid`,`name`,`group`", "fid", 'name'));
    $.addCache(store, 'region', $.cacheModel("/apis/web/region?page=0&field=`rid`,`name`,`fid`,`group`", "rid", 'name'));
    $.addCache(store, 'taocan', $.cacheModel("/apis/number/taocan?page=0&field=`tid`,`sid`,`name`", "tid", 'name'));
    $.addCache(store, 'standard', $.cacheModel("/apis/number/standard?page=0&field=`sid`,`name`,`segment`,`kid`,`name`", "sid", 'name'));
    $.addCache(store, 'yuyi', $.cacheModel("/apis/number/yuyi?page=0&field=`yid`,`name`", "yid", 'name'));
    $.addCache(store, 'city', $.cacheModel("/apis/web/region?page=0&group=市&field=`rid`,`name`,`fid`&sort=`display` asc,`name` asc", "rid", 'name'));
    $.addCache(store, 'gongHuoShang', $.cacheModel("/apis/user?page=0&group=供应商&field=`uid`,`nickName`&sort=`nickname` asc", "uid", 'nickName'));
    $.addCache(store, 'daiLiShang', $.cacheModel("/apis/user?page=0&group=代销&field=`uid`,`nickName`&sort=`nickname` asc", "uid", 'nickName'));
    $.addCache(store, 'keFu', $.cacheModel("/apis/user?page=0&group=客服&field=`uid`,`name`&sort=`name` asc", "uid", 'name'));
    $.addCache(store, 'mailMan', $.cacheModel("/apis/user?page=0&group=配送&field= `uid`,`name`&sort=`name` asc", "uid", 'name'));
    $.addCache(store, 'channel', $.cacheModel("/apis/web/channel?page=0&field=`cid`,`name`,`fid`,`tpltype`", "cid", 'name'));
    $.addCache(store, 'kind', $.cacheModel("/apis/number/kind?page=0&field=`kid`,`name`", "kid", 'name'));
    $.addCache(store, 'platform', $.cacheModel("/apis/web/platform?page=0&field=`pid`,`name`", "pid", 'name'));
    $.addCache(store, 'buy', $.cacheModel("/apis/number/buy?page=0&field=`bid`,`name`", "bid", 'name'));
    $.addCache(store, 'recommend', $.cacheModel("/apis/number/recommend?page=0&field=`rid`,`title`", "rid", 'title'));
    $.addCache(store, 'power', $.cacheModel("/apis/user/power?page=0&field=*", "pid", 'name'));
    $.addCache(store, 'pricerange', $.cacheModel("/apis/number/pricerange?page=0&field=*", "pid", 'name'));

    var states = {
        user: {}
    };

    var methods = {
        updateCaches: function updateCaches() {
            $.postAsync("/update/data");
        }
    };
    $.eachObj(store.state, states, true);

    var storer = new vuex.Store(store); //生成缓存器
    return storer; //返回缓存器
});