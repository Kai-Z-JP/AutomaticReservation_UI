﻿using AutomaticReservation_UI.Common;
using AutomaticReservation_UI.Domain;
using AutomaticReservation_UI.Model;
using AutomaticReservation_UI.ToyokoInn;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;

namespace AutomaticReservation_UI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region コマンド・プロパティ
        private readonly IDataService _dataService;

        private RelayCommand _btnExecute;
        /// <summary>
        /// 予約実行コマンド
        /// </summary>
        public RelayCommand BtnExecute
        {
            get
            {
                if (_btnExecute == null)
                {
                    _btnExecute = new RelayCommand(Execute, CanExecute);
                }
                return _btnExecute;
            }
        }

        private RelayCommand _btnConfigure;
        /// <summary>
        /// 予約設定コマンド
        /// </summary>
        public RelayCommand BtnConfigure
        {
            get
            {
                if (_btnConfigure == null)
                {
                    _btnConfigure = new RelayCommand(ExecuteConfigure, CanExecuteConfigure);
                }
                return _btnConfigure;
            }
        }

        private RelayCommand _btnHotelUpdate;
        /// <summary>
        /// ホテル情報更新コマンド
        /// </summary>
        public RelayCommand BtnHotelUpdate
        {
            get
            {
                if (_btnHotelUpdate == null)
                {
                    _btnHotelUpdate = new RelayCommand(ExecuteHotelUpdate, CanExecuteHotelUpdate);
                }
                return _btnHotelUpdate;
            }
        }

        private string _hotelID;
        /// <summary>
        /// ホテルID
        /// </summary>
        public string HotelID
        {
            get
            {
                return _hotelID;
            }
            set
            {
                _hotelID = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _checkinDate;
        /// <summary>
        /// チェックイン日付
        /// </summary>
        public DateTime CheckinDate
        {
            get
            {
                return _checkinDate;
            }
            set
            {
                _checkinDate = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<RoomType> _colRoomType;
        /// <summary>
        /// 部屋タイプコレクション
        /// </summary>
        public ObservableCollection<RoomType> ColRoomType
        {
            get
            {
                return _colRoomType;
            }
            set
            {
                _colRoomType = value;
                RaisePropertyChanged();
            }
        }

        private RoomType _selectedRoomType;
        /// <summary>
        /// 選択された部屋タイプ
        /// </summary>
        public RoomType SelectedRoomType
        {
            get
            {
                return _selectedRoomType;
            }
            set
            {
                _selectedRoomType = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<CheckinTime> _colCheckinTime;
        /// <summary>
        /// チェックイン予定時刻コレクション
        /// </summary>
        public ObservableCollection<CheckinTime> ColCheckinTime
        {
            get
            {
                return _colCheckinTime;
            }
            set
            {
                _colCheckinTime = value;
                RaisePropertyChanged();
            }
        }

        private CheckinTime _selectedCheckinTime;
        /// <summary>
        /// 選択されたチェックイン予定時刻
        /// </summary>
        public CheckinTime SelectedCheckinTime
        {
            get
            {
                return _selectedCheckinTime;
            }
            set
            {
                _selectedCheckinTime = value;
                RaisePropertyChanged();
            }
        }

        private bool _chkNoSmoking;
        /// <summary>
        /// 禁煙を検索する
        /// </summary>
        public bool ChkNoSmoking
        {
            get
            {
                return _chkNoSmoking;
            }
            set
            {
                _chkNoSmoking = value;
                RaisePropertyChanged();
            }
        }

        private bool _chkSmoking;
        /// <summary>
        /// 喫煙を検索する
        /// </summary>
        public bool ChkSmoking
        {
            get
            {
                return _chkSmoking;
            }
            set
            {
                _chkSmoking = value;
                RaisePropertyChanged();
            }
        }

        private bool _isSmokingFirst;
        /// <summary>
        /// 喫煙を優先する
        /// </summary>
        public bool IsSmokingFirst
        {
            get
            {
                return _isSmokingFirst;
            }
            set
            {
                _isSmokingFirst = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ReservationControlViewModel> ReservationList { get; set; } = new ObservableCollection<ReservationControlViewModel>();
        private ReservationControlViewModel _reservationViewModel;
        public ReservationControlViewModel ReservationViewModel
        {
            get
            {
                return _reservationViewModel;
            }
            set
            {
                _reservationViewModel = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                });
            CheckinDate = DateTime.Now.AddDays(1);
            ChkNoSmoking = true;
            ChkSmoking = false;

            var displayTuple = Load();
            ColRoomType = displayTuple.Item1;
            ColCheckinTime = displayTuple.Item2;
        }

        /// <summary>
        /// コマンドを実行
        /// </summary>
        public void Execute()
        {
            // 検索クラス作成
            var finder = new ProcessFormat()
            {
                HotelID = new Hotel()
                {
                    // ----- ----- ----- ----- -----
                    // Test Code
                    // ----- ----- ----- ----- -----
                    HotelID = HotelID,
                    HotelName = "品川大井町",
                    PrefCode = 14
                },
                CheckinDate = CheckinDate,
                Type = SelectedRoomType,
                CheckinValue = SelectedCheckinTime,
                EnableNoSmoking = ChkNoSmoking,
                EnableSmoking = ChkSmoking,
                SmokingFirst = IsSmokingFirst
            };
            var item = new ReservationControlViewModel(finder);
            ReservationList.Add(item);
        }
        /// <summary>
        /// コマンドが実行可能かどうかを判定
        /// </summary>
        /// <returns></returns>
        public bool CanExecute()
        {
            return !(String.IsNullOrEmpty(HotelID)) && !(!ChkNoSmoking && !ChkSmoking) && !(SelectedRoomType is null) && !(SelectedCheckinTime is null);
        }

        public void ExecuteConfigure()
        {

        }
        public bool CanExecuteConfigure()
        {
            return false;
        }

        public void ExecuteHotelUpdate()
        {
        }
        public bool CanExecuteHotelUpdate()
        {
            return true;
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {

        }

        /// <summary>
        /// データをXMLから読み込む
        /// </summary>
        /// <returns></returns>
        private Tuple<ObservableCollection<RoomType>, ObservableCollection<CheckinTime>> Load()
        {
            var ret1 = new ObservableCollection<RoomType>();
            try
            {
                // ファイルが存在する
                ret1 = XmlConverter.DeSerializeToCol<RoomType>(String.Format(@"{0}\RoomType.xml", SiteConfig.BASE_DIR));
            }
            catch
            {
                // ファイルが存在しない
                XmlConverter.SerializeFromCol(ColRoomType, String.Format(@"{0}\RoomType.xml", SiteConfig.BASE_DIR));
            }

            var ret2 = new ObservableCollection<CheckinTime>();
            try
            {
                // ファイルが存在する
                ret2 = XmlConverter.DeSerializeToCol<CheckinTime>(String.Format(@"{0}\CheckinTime.xml", SiteConfig.BASE_DIR));
            }
            catch
            {
                // ファイルが存在しない
                XmlConverter.SerializeFromCol(ColCheckinTime, String.Format(@"{0}\CheckinTime.xml", SiteConfig.BASE_DIR));
            }

            return Tuple.Create(ret1, ret2);
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}