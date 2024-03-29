﻿/*
MIT License

Copyright(c) 2020 Kyle Givler
https://github.com/JoyfulReaper

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using ConsignmentShopLibrary;
using ConsignmentShopLibrary.Data;
using ConsignmentShopLibrary.Data.MSSQL;
using ConsignmentShopLibrary.Data.SQLite;
using ConsignmentShopLibrary.DataAccess;
using ConsignmentShopLibrary.DataAccess.MSSQL;
using ConsignmentShopLibrary.DataAccess.SQLite;
using ConsignmentShopLibrary.Services;
using ConsignmentShopUI.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsignmentShopUI
{
    internal static class Bootstrap
    {
        public static IServiceProvider Initialize()
        {
            var container = new ServiceCollection();

            IConfig config = new Config();
            config.Initiliaze();
            var db = config.DBType;

            if (db == DatabaseType.MSSQL)
            {
                container
                    .AddSingleton<IDataAccess, SqlDb>()
                    .AddSingleton<IItemData, ItemData>()
                    .AddSingleton<IStoreData, StoreData>()
                    .AddSingleton<IVendorData, VendorData>()
                    .AddSingleton<IItemService, ItemService>()
                    .AddSingleton<IVendorService, VendorService>();
            }
            else if (db == DatabaseType.SQLite)
            {
                container
                    .AddSingleton<IDataAccess, SQLiteDB>()
                    .AddSingleton<IItemData, SQLiteItemData>()
                    .AddSingleton<IStoreData, SQLiteStoreData>()
                    .AddSingleton<IVendorData, SQLiteVendorData>()
                    .AddSingleton<IItemService, ItemService>()
                    .AddSingleton<IVendorService, VendorService>();
            }

            container
                .AddSingleton(_ => config)
                .AddTransient<ConsignmentShop>()
                .AddTransient<ItemMaintFrm>()
                .AddTransient<VendorMaintFrm>()
                .AddSingleton<ItemMaintFormFactory>()
                .AddSingleton<VendorMaintFormFactory>()
                .AddSingleton<IServiceCollection>(_ => container);

            return container.BuildServiceProvider();
        }
    }
}