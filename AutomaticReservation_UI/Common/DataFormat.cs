﻿using CsvHelper.Configuration;
using System;

namespace AutomaticReservation_UI.Common
{
    /// <summary>
    /// 共通パス設定
    /// </summary>
    public static class CommonPath
    {
        /// <summary>
        /// 共通設定ディレクトリ
        /// </summary>
        public static string CommonDir = String.Format(@"{0}\AutomaticReservation_UI\common", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
    }

    /// <summary>
    /// 都道府県コードクラス
    /// </summary>
    public class PrefCode
    {
        /// <summary>
        /// 都道府県コード
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 都道府県名
        /// </summary>
        public string PrefName { get; set; }
    }
    /// <summary>
    /// 都道府県コードのCSVマッピングクラス
    /// </summary>
    public sealed class PrefCodeMap : ClassMap<PrefCode>
    {
        public PrefCodeMap()
        {
            Map(x => x.ID).Index(0);
            Map(x => x.PrefName).Index(1);
        }
    }
}