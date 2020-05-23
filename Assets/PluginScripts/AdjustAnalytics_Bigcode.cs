//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using com.adjust.sdk;
//using UnityEngine.Purchasing;


//public class AdjustAnalytics_Bigcode : MonoBehaviour
//{
//    public string AppToken;

//    public static AdjustAnalytics_Bigcode Instance;

//    public Dictionary<string,string> eventsKeys = new Dictionary<string, string>();

//    AdjustEnvironment environment = AdjustEnvironment.Production;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    private void Start()
//    {
//        AdjustConfig config = new AdjustConfig(AppToken, environment, true);
//        config.setLogLevel(AdjustLogLevel.Suppress);
//        Adjust.start(config);

//        loadEvents(eventsKeys);


//    }

//    public void trackPurchase(Product product)
//    {
//        AdjustEvent adjustEvent = new AdjustEvent(product.definition.id);
//        adjustEvent.setRevenue((double)product.metadata.localizedPrice, product.metadata.isoCurrencyCode);
//        adjustEvent.setTransactionId(product.transactionID);
//        Adjust.trackEvent(adjustEvent);
//    }

//    public void LogEvent(string eventName)
//    {
//        if (eventsKeys.ContainsKey(eventName))
//        {
//            AdjustEvent adjustEvent = new AdjustEvent(eventsKeys[eventName]);
//            Adjust.trackEvent(adjustEvent);

//            Debug.LogError(eventsKeys[eventName]+"called");
//        }

//    }

//    public void LogEvent(string eventName, int levelNo)
//    {
//        if (eventsKeys.ContainsKey(eventName))
//        {
//            AdjustEvent adjustEvent = new AdjustEvent(eventsKeys[eventName]);
//            adjustEvent.addCallbackParameter("level", levelNo.ToString());
//            Adjust.trackEvent(adjustEvent);
//        }

//    }

//    void loadEvents(Dictionary<string,string> hashtable)
//    {
//        // These are Hill Car Stunts 2020 Game Events
//        eventsKeys.Add("ourad_page", "dig5ou");
//        eventsKeys.Add("ourad_page_clicked", "72mnie");
//        eventsKeys.Add("ourad_page_close", "pl9o02");
//        eventsKeys.Add("menu_page", "hkzi92");
//        eventsKeys.Add("menu_page_singleplayer", "3eerip");
//        eventsKeys.Add("menu_page_multiplayer", "t39c6y");
//        eventsKeys.Add("menu_page_garage", "sd4scg");
//        eventsKeys.Add("menu_page_specialevents", "gt656z");
//        eventsKeys.Add("menu_page_vip", "kle86h");
//        eventsKeys.Add("menu_page_megasale", "vhc3lk");
//        eventsKeys.Add("menu_page_mg", "hhuohd");
//        eventsKeys.Add("menu_page_store", "2gka9s");
//        eventsKeys.Add("menu_page_settings", "186cxk");
//        eventsKeys.Add("menu_page_watchvideo", "w0dd0j");
//        eventsKeys.Add("menu_page_spin", "6sutar");
//        eventsKeys.Add("menu_page_dailybonus", "7hduos");
//        eventsKeys.Add("menu_page_packs", "c9pw2r");
//        eventsKeys.Add("packs_page_freecoins", "dpkwp3");
//        eventsKeys.Add("packs_page_magnet", "fs21mq");
//        eventsKeys.Add("packs_page_booster", "fiosgy");
//        eventsKeys.Add("packs_page_1", "u1n1wp");
//        eventsKeys.Add("packs_page_2", "e2zv7n");
//        eventsKeys.Add("packs_page_3", "5x4orb");
//        eventsKeys.Add("packs_page_4", "bkmn0p");
//        eventsKeys.Add("packs_page_5", "oz1c2m");
//        eventsKeys.Add("packs_page_6", "9i9vq5");
//        eventsKeys.Add("vip_page", "wo74nj");
//        eventsKeys.Add("vip_page_cancel", "7roc13");
//        eventsKeys.Add("vip_page_weekly", "6ms4e1");
//        eventsKeys.Add("vip_page_monthly", "p1jqfp");
//        eventsKeys.Add("vip_page_yearly", "4b4qau");
//        eventsKeys.Add("store_page_unlockalllevels", "czppvm");
//        eventsKeys.Add("store_page_unlockallmodes", "79eakc");
//        eventsKeys.Add("store_page_cars", "2hgplm");
//        eventsKeys.Add("store_page_everything", "658x8v");
//        eventsKeys.Add("dailybonus_page", "oud62p");
//        eventsKeys.Add("dailybonus_page_day_1", "pnr373");
//        eventsKeys.Add("dailybonus_page_day_2", "cmw378");
//        eventsKeys.Add("dailybonus_page_day_3", "btzo9g");
//        eventsKeys.Add("dailybonus_page_day_4", "qcav7q");
//        eventsKeys.Add("dailybonus_page_day_5", "4jaly6");
//        eventsKeys.Add("dailybonus_page_day_6", "9fkg0s");
//        eventsKeys.Add("dailybonus_page_day_7", "1qczpx");
//        eventsKeys.Add("vehicles_page", "iec7qq");
//        eventsKeys.Add("vehicles_page_1", "cjywnf");
//        eventsKeys.Add("vehicles_page_2", "ybfb6m");
//        eventsKeys.Add("vehicles_page_3", "d0g0cq");
//        eventsKeys.Add("vehicles_page_4", "vgmzjy");
//        eventsKeys.Add("vehicles_page_5", "yp0roh");
//        eventsKeys.Add("vehicles_page_6", "9vsyf5");
//        eventsKeys.Add("vehicles_page_7", "bjhc3z");
//        eventsKeys.Add("vehicles_page_8", "urv2xq");
//        eventsKeys.Add("vehicles_page_9", "bu0hb4");
//        eventsKeys.Add("vehicles_page_10", "e3xwo0");
//        eventsKeys.Add("vehicles_page_11", "81d1by");
//        eventsKeys.Add("vehicles_page_12", "vcqphw");
//        eventsKeys.Add("vehicles_page_13", "vo2vlo");
//        eventsKeys.Add("vehicles_page_14", "gnpok2");
//        eventsKeys.Add("vehicles_page_15", "y2x0s8");
//        eventsKeys.Add("vehicles_page_16", "gahfkd");
//        eventsKeys.Add("vehicles_page_17", "taav06");
//        eventsKeys.Add("vehicles_page_18", "ormf4x");
//        eventsKeys.Add("vehicles_page_19", "m2kll0");
//        eventsKeys.Add("vehicles_page_20", "st28ln");
//        eventsKeys.Add("vehicles_page_21", "qiejnw");
//        eventsKeys.Add("vehicles_page_22", "e6rodj");
//        eventsKeys.Add("vehicles_page_23", "unktd2");
//        eventsKeys.Add("vehicles_page_24", "w51r72");
//        eventsKeys.Add("vehicles_page_25", "xq07ee");
//        eventsKeys.Add("vehicles_page_26", "b3rxch");
//        eventsKeys.Add("vehicles_page_27", "h0r314");
//        eventsKeys.Add("levels_page", "1xoxs1");
//        eventsKeys.Add("levels_page_unlock_levels", "he1hra");
//        eventsKeys.Add("levels_page_world_1", "qandjz");
//        eventsKeys.Add("levels_page_world_1_L_1", "jxvyya");
//        eventsKeys.Add("levels_page_world_1_L_2", "r1auys");
//        eventsKeys.Add("levels_page_world_1_L_3", "8qw9af");
//        eventsKeys.Add("levels_page_world_1_L_4", "md4uo1");
//        eventsKeys.Add("levels_page_world_1_L_5", "fs7qdo");
//        eventsKeys.Add("levels_page_world_1_L_6", "r529lm");
//        eventsKeys.Add("levels_page_world_1_L_7", "datgjq");
//        eventsKeys.Add("levels_page_world_1_L_8", "kyi32u");
//        eventsKeys.Add("levels_page_world_1_L_9", "gib1uc");
//        eventsKeys.Add("levels_page_world_1_L_10", "sg0l9r");
//        eventsKeys.Add("levels_page_world_2", "i7294e");
//        eventsKeys.Add("levels_page_world_2_L_1", "tqndzw");
//        eventsKeys.Add("levels_page_world_2_L_2", "ec3e3t");
//        eventsKeys.Add("levels_page_world_2_L_3", "s5c28f");
//        eventsKeys.Add("levels_page_world_2_L_4", "xh7vz5");
//        eventsKeys.Add("levels_page_world_2_L_5", "l5ftrs");
//        eventsKeys.Add("levels_page_world_2_L_6", "p5p5o6");
//        eventsKeys.Add("levels_page_world_2_L_7", "pk7cpg");
//        eventsKeys.Add("levels_page_world_2_L_8", "tpwudx");
//        eventsKeys.Add("levels_page_world_2_L_9", "dw3wnb");
//        eventsKeys.Add("levels_page_world_2_L_10", "lyawwj");
//        eventsKeys.Add("levels_page_world_3", "2pd2li");
//        eventsKeys.Add("levels_page_world_3_L_1", "prntrw");
//        eventsKeys.Add("levels_page_world_3_L_2", "4fdokq");
//        eventsKeys.Add("levels_page_world_3_L_3", "r6wi7j");
//        eventsKeys.Add("levels_page_world_3_L_4", "ht3d3k");
//        eventsKeys.Add("levels_page_world_3_L_5", "92tyg3");
//        eventsKeys.Add("levels_page_world_3_L_6", "tufh9a");
//        eventsKeys.Add("levels_page_world_3_L_7", "1kttsc");
//        eventsKeys.Add("levels_page_world_3_L_8", "6pzkpf");
//        eventsKeys.Add("levels_page_world_3_L_9", "tcldfp");
//        eventsKeys.Add("levels_page_world_3_L_10", "ehha1k");
//        eventsKeys.Add("levels_page_world_4", "36sk66");
//        eventsKeys.Add("levels_page_world_4_L_1", "9xytse");
//        eventsKeys.Add("levels_page_world_4_L_2", "gps1uu");
//        eventsKeys.Add("levels_page_world_4_L_3", "upuznu");
//        eventsKeys.Add("levels_page_world_4_L_4", "o0wmtc");
//        eventsKeys.Add("levels_page_world_4_L_5", "ck7jk1");
//        eventsKeys.Add("levels_page_world_4_L_6", "wus2p3");
//        eventsKeys.Add("levels_page_world_4_L_7", "xpskq3");
//        eventsKeys.Add("levels_page_world_4_L_8", "91cp21");
//        eventsKeys.Add("levels_page_world_4_L_9", "qb1kxw");
//        eventsKeys.Add("levels_page_world_4_L_10", "c7k5ek");
//        eventsKeys.Add("levels_page_world_5", "ighs5o");
//        eventsKeys.Add("levels_page_world_5_L_1", "tx63ha");
//        eventsKeys.Add("levels_page_world_5_L_2", "x7mfd3");
//        eventsKeys.Add("levels_page_world_5_L_3", "cbud2l");
//        eventsKeys.Add("levels_page_world_5_L_4", "omyi0q");
//        eventsKeys.Add("levels_page_world_5_L_5", "ra728c");
//        eventsKeys.Add("levels_page_world_5_L_6", "vp97qo");
//        eventsKeys.Add("levels_page_world_5_L_7", "lindmc");
//        eventsKeys.Add("levels_page_world_5_L_8", "gums0q");
//        eventsKeys.Add("levels_page_world_5_L_9", "llpg3j");
//        eventsKeys.Add("levels_page_world_5_L_10", "mvb5fr");
//        eventsKeys.Add("levels_page_world_6", "cvbez8");
//        eventsKeys.Add("levels_page_world_6_L_1", "wzmypv");
//        eventsKeys.Add("levels_page_world_6_L_2", "ywxj5y");
//        eventsKeys.Add("levels_page_world_6_L_3", "ej4v33");
//        eventsKeys.Add("levels_page_world_6_L_4", "ngzvje");
//        eventsKeys.Add("levels_page_world_6_L_5", "i1mliw");
//        eventsKeys.Add("levels_page_world_6_L_6", "k5kyal");
//        eventsKeys.Add("levels_page_world_6_L_7", "r10enl");
//        eventsKeys.Add("levels_page_world_6_L_8", "8yp9vn");
//        eventsKeys.Add("levels_page_world_6_L_9", "1go7eq");
//        eventsKeys.Add("levels_page_world_6_L_10", "rvv35o");
//        eventsKeys.Add("LC_page", "tdfw4r");
//        eventsKeys.Add("LC_page_2xreward", "ctaes2");
//        eventsKeys.Add("LC_page_unlockallvehicles", "11ldv5");
//        eventsKeys.Add("LC_page_mg", "2roeq8");
//        eventsKeys.Add("LC_page_mmg", "mcxe6r");
//        eventsKeys.Add("LC_page_share", "ueb5ss");
//        eventsKeys.Add("LC_page_world_1_L_1", "9hczzt");
//        eventsKeys.Add("LC_page_world_1_L_2", "cv6ygu");
//        eventsKeys.Add("LC_page_world_1_L_3", "e13mmj");
//        eventsKeys.Add("LC_page_world_1_L_4", "8ebm0j");
//        eventsKeys.Add("LC_page_world_1_L_5", "dryyco");
//        eventsKeys.Add("LC_page_world_1_L_6", "v8odvv");
//        eventsKeys.Add("LC_page_world_1_L_7", "7xuyid");
//        eventsKeys.Add("LC_page_world_1_L_8", "qrn2tv");
//        eventsKeys.Add("LC_page_world_1_L_9", "dhgnbr");
//        eventsKeys.Add("LC_page_world_1_L_10", "cv3vap");
//        eventsKeys.Add("LC_page_world_2_L_1", "kvq1pk");
//        eventsKeys.Add("LC_page_world_2_L_2", "dpdhcw");
//        eventsKeys.Add("LC_page_world_2_L_3", "sp97os");
//        eventsKeys.Add("LC_page_world_2_L_4", "pgn3p6");
//        eventsKeys.Add("LC_page_world_2_L_5", "sg1vh7");
//        eventsKeys.Add("LC_page_world_2_L_6", "o16mb0");
//        eventsKeys.Add("LC_page_world_2_L_7", "8ze3jw");
//        eventsKeys.Add("LC_page_world_2_L_8", "opbune");
//        eventsKeys.Add("LC_page_world_2_L_9", "vy10hi");
//        eventsKeys.Add("LC_page_world_2_L_10", "xi7ia3");
//        eventsKeys.Add("LC_page_world_3_L_1", "1qxajb");
//        eventsKeys.Add("LC_page_world_3_L_2", "tw124l");
//        eventsKeys.Add("LC_page_world_3_L_3", "3yde4t");
//        eventsKeys.Add("LC_page_world_3_L_4", "ngyag5");
//        eventsKeys.Add("LC_page_world_3_L_5", "f3cotr");
//        eventsKeys.Add("LC_page_world_3_L_6", "fdn53w");
//        eventsKeys.Add("LC_page_world_3_L_7", "es5n33");
//        eventsKeys.Add("LC_page_world_3_L_8", "tner8q");
//        eventsKeys.Add("LC_page_world_3_L_9", "hjlb25");
//        eventsKeys.Add("LC_page_world_3_L_10", "lvwii1");
//        eventsKeys.Add("LC_page_world_4_L_1", "j2sa23");
//        eventsKeys.Add("LC_page_world_4_L_2", "took36");
//        eventsKeys.Add("LC_page_world_4_L_3", "1kqy0b");
//        eventsKeys.Add("LC_page_world_4_L_4", "y0oygd");
//        eventsKeys.Add("LC_page_world_4_L_5", "9dwljm");
//        eventsKeys.Add("LC_page_world_4_L_6", "ndq53c");
//        eventsKeys.Add("LC_page_world_4_L_7", "odw5tj");
//        eventsKeys.Add("LC_page_world_4_L_8", "za93iv");
//        eventsKeys.Add("LC_page_world_4_L_9", "y8cur1");
//        eventsKeys.Add("LC_page_world_4_L_10", "lueycd");
//        eventsKeys.Add("LC_page_world_5_L_1", "v51ncs");
//        eventsKeys.Add("LC_page_world_5_L_2", "spztpx");
//        eventsKeys.Add("LC_page_world_5_L_3", "n13snu");
//        eventsKeys.Add("LC_page_world_5_L_4", "ifyeek");
//        eventsKeys.Add("LC_page_world_5_L_5", "7j5eaz");
//        eventsKeys.Add("LC_page_world_5_L_6", "10vuy2");
//        eventsKeys.Add("LC_page_world_5_L_7", "vp7p46");
//        eventsKeys.Add("LC_page_world_5_L_8", "jktrng");
//        eventsKeys.Add("LC_page_world_5_L_9", "e5j6gu");
//        eventsKeys.Add("LC_page_world_5_L_10", "fbhqo1");
//        eventsKeys.Add("LC_page_world_6_L_1", "a97o4h");
//        eventsKeys.Add("LC_page_world_6_L_2", "3xxghq");
//        eventsKeys.Add("LC_page_world_6_L_3", "ntf5za");
//        eventsKeys.Add("LC_page_world_6_L_4", "uqhwnl");
//        eventsKeys.Add("LC_page_world_6_L_5", "fpivr2");
//        eventsKeys.Add("LC_page_world_6_L_6", "2djud5");
//        eventsKeys.Add("LC_page_world_6_L_7", "d1vd00");
//        eventsKeys.Add("LC_page_world_6_L_8", "5ig3wo");
//        eventsKeys.Add("LC_page_world_6_L_9", "hwhxbq");
//        eventsKeys.Add("LC_page_world_6_L_10", "nu3nqz");
//        eventsKeys.Add("LF_page", "y72f0m");
//        eventsKeys.Add("LF_page_home", "coeva5");
//        eventsKeys.Add("LF_page_mg", "2h7zy1");
//        eventsKeys.Add("LF_page_unlockalllevels", "aguxao");
//        eventsKeys.Add("LF_page_mmg", "1u30f3");
//        eventsKeys.Add("LF_page_world_1_L_1", "r2oio1");
//        eventsKeys.Add("LF_page_world_1_L_2", "p05k5c");
//        eventsKeys.Add("LF_page_world_1_L_3", "6p14qu");
//        eventsKeys.Add("LF_page_world_1_L_4", "dsgqy6");
//        eventsKeys.Add("LF_page_world_1_L_5", "irbb0g");
//        eventsKeys.Add("LF_page_world_1_L_6", "at07rf");
//        eventsKeys.Add("LF_page_world_1_L_7", "3x0w13");
//        eventsKeys.Add("LF_page_world_1_L_8", "gnrku4");
//        eventsKeys.Add("LF_page_world_1_L_9", "vf3pa1");
//        eventsKeys.Add("LF_page_world_1_L_10", "j7t57i");
//        eventsKeys.Add("LF_page_world_2_L_1", "3ghx5u");
//        eventsKeys.Add("LF_page_world_2_L_2", "dx6lg1");
//        eventsKeys.Add("LF_page_world_2_L_3", "sxpn74");
//        eventsKeys.Add("LF_page_world_2_L_4", "gv804k");
//        eventsKeys.Add("LF_page_world_2_L_5", "3onh0l");
//        eventsKeys.Add("LF_page_world_2_L_6", "zigoxt");
//        eventsKeys.Add("LF_page_world_2_L_7", "s0nucp");
//        eventsKeys.Add("LF_page_world_2_L_8", "huwlfg");
//        eventsKeys.Add("LF_page_world_2_L_9", "kmjr3y");
//        eventsKeys.Add("LF_page_world_2_L_10", "v5qni4");
//        eventsKeys.Add("LF_page_world_3_L_1", "6ekej9");
//        eventsKeys.Add("LF_page_world_3_L_2", "i0rwlg");
//        eventsKeys.Add("LF_page_world_3_L_3", "ftsh58");
//        eventsKeys.Add("LF_page_world_3_L_4", "gjq0er");
//        eventsKeys.Add("LF_page_world_3_L_5", "8vkf6e");
//        eventsKeys.Add("LF_page_world_3_L_6", "1hsq7k");
//        eventsKeys.Add("LF_page_world_3_L_7", "dzcdh8");
//        eventsKeys.Add("LF_page_world_3_L_8", "hw2g4g");
//        eventsKeys.Add("LF_page_world_3_L_9", "ftqqk3");
//        eventsKeys.Add("LF_page_world_3_L_10", "l8kms8");
//        eventsKeys.Add("LF_page_world_4_L_1", "dzafjf");
//        eventsKeys.Add("LF_page_world_4_L_2", "lry597");
//        eventsKeys.Add("LF_page_world_4_L_3", "saleiw");
//        eventsKeys.Add("LF_page_world_4_L_4", "gwsb0r");
//        eventsKeys.Add("LF_page_world_4_L_5", "qjvoa6");
//        eventsKeys.Add("LF_page_world_4_L_6", "3mo7gk");
//        eventsKeys.Add("LF_page_world_4_L_7", "md38oy");
//        eventsKeys.Add("LF_page_world_4_L_8", "2wkj8g");
//        eventsKeys.Add("LF_page_world_4_L_9", "54efn3");
//        eventsKeys.Add("LF_page_world_4_L_10", "gqgi1b");
//        eventsKeys.Add("LF_page_world_5_L_1", "asduv4");
//        eventsKeys.Add("LF_page_world_5_L_2", "57dsbi");
//        eventsKeys.Add("LF_page_world_5_L_3", "pm1pmx");
//        eventsKeys.Add("LF_page_world_5_L_4", "42trjl");
//        eventsKeys.Add("LF_page_world_5_L_5", "ihraiy");
//        eventsKeys.Add("LF_page_world_5_L_6", "ct7gc0");
//        eventsKeys.Add("LF_page_world_5_L_7", "pqqeo3");
//        eventsKeys.Add("LF_page_world_5_L_8", "iu2iy1");
//        eventsKeys.Add("LF_page_world_5_L_9", "3g6xxo");
//        eventsKeys.Add("LF_page_world_5_L_10", "3wh0ec");
//        eventsKeys.Add("LF_page_world_6_L_1", "fs9exj");
//        eventsKeys.Add("LF_page_world_6_L_2", "qzy05r");
//        eventsKeys.Add("LF_page_world_6_L_3", "3awkki");
//        eventsKeys.Add("LF_page_world_6_L_4", "9atay4");
//        eventsKeys.Add("LF_page_world_6_L_5", "h480t2");
//        eventsKeys.Add("LF_page_world_6_L_6", "3bc3o1");
//        eventsKeys.Add("LF_page_world_6_L_7", "vo4p1d");
//        eventsKeys.Add("LF_page_world_6_L_8", "94casg");
//        eventsKeys.Add("LF_page_world_6_L_9", "x2oc9c");
//        eventsKeys.Add("LF_page_world_6_L_10", "6180mm");
//        eventsKeys.Add("multiplayer_page_LC_L_", "hcer44");
//        eventsKeys.Add("multiplayer_page_LC_replay", "v2gvo6");
//        eventsKeys.Add("multiplayer_page_LC_share", "14lv22");
//        eventsKeys.Add("multiplayer_page_LF_L_", "ppp6nq");
//        eventsKeys.Add("multiplayer_page_LF_retry", "7nevff");
//        eventsKeys.Add("multiplayer_page_LF_home", "vh6chl");
//        eventsKeys.Add("rating_page", "reye6t");
//        eventsKeys.Add("rating_page_thankyou", "3flblh");
//        eventsKeys.Add("rating_page_rate", "naag5h");
//        eventsKeys.Add("rating_page_notnow", "cdbhtt");
//        eventsKeys.Add("specialevents_page", "4fvcxv");
//        eventsKeys.Add("specialevents_page_wednesdayevent", "8xooyj");
//        eventsKeys.Add("specialevents_page_saturdayevent", "uubhf5");
//        eventsKeys.Add("megasale_page", "i2bmn7");
//        eventsKeys.Add("megasale_page_pack1", "w1quwx");
//        eventsKeys.Add("megasale_page_pack2", "ua4yq2");
//        eventsKeys.Add("megasale_page_pack3", "qnjqeo");
//        eventsKeys.Add("megasale_page_pack4", "x9q6t2");
//        eventsKeys.Add("megasale_page_pack5", "w2x0pe");
//        eventsKeys.Add("megasale_page_pack6", "atv9ur");
//        eventsKeys.Add("megasale_page_pack7", "5udon0");
//        eventsKeys.Add("pause_page", "227c23");
//        eventsKeys.Add("pause_page_home", "joj458");
//        eventsKeys.Add("pause_page_play", "oq0gw8");
//        eventsKeys.Add("pause_page_retry", "8rm165");
//        eventsKeys.Add("setting_page", "qduvjw");
//        eventsKeys.Add("setting_page_language_clicked", "8t4m38");
//        eventsKeys.Add("setting_page_sound_clicked", "u6da2r");
//        eventsKeys.Add("setting_page_music_clicked", "j3ysse");
//        eventsKeys.Add("unlock_pack_page", "fq9xff");
//        eventsKeys.Add("unlock_pack_page_1", "1zmsk6");
//        eventsKeys.Add("unlock_pack_page_2", "zbfnq3");
//        eventsKeys.Add("unlock_pack_page_3", "rs8jy7");
//        eventsKeys.Add("unlock_pack_page_4", "6intfz");
//        eventsKeys.Add("unlock_pack_page_5", "1q7bli");
//        eventsKeys.Add("unlock_pack_page_6", "osd6ks");
//        eventsKeys.Add("unlock_store_page_unlockalllevels", "mixe7y");
//        eventsKeys.Add("unlock_store_page_unlockallmodes", "ii5j8j");
//        eventsKeys.Add("unlock_store_page_cars", "6vbqxq");
//        eventsKeys.Add("unlock_store_page_everything", "h5s000");
//        eventsKeys.Add("unlock_vip_page_weekly", "vo0tcn");
//        eventsKeys.Add("unlock_vip_page_monthly", "fncx6i");
//        eventsKeys.Add("unlock_vip_page_yearly", "i93vzp");
//        eventsKeys.Add("profile_page", "a8xqsv");
//        eventsKeys.Add("profile_page_cards", "ye9vs8");
//        eventsKeys.Add("profile_page_profile", "un0qex");
//        eventsKeys.Add("profile_page_cards_unlocked_all", "ibl7vl");
//    }

//}
