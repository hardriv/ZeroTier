using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ZeroTier.ViewModels.MemberModels
{
    public class MemberConfigViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _activeBridge;
        public bool ActiveBridge
        {
            get => _activeBridge;
            set
            {
                if (_activeBridge != value)
                {
                    _activeBridge = value;
                    OnPropertyChanged();
                }
            }
        }

        public required string Address { get; set; }

        private bool _authorized;
        public bool Authorized
        {
            get => _authorized;
            set
            {
                if (_authorized != value)
                {
                    _authorized = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<object>? Capabilities { get; set; }
        public DateTime CreationTime { get; set; }
        public required string Id { get; set; }
        public required string Identity { get; set; }

        private string _ipAssignment;
        public required string IpAssignment
        {
            get => _ipAssignment;
            set
            {
                if (_ipAssignment != value)
                {
                    _ipAssignment = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime LastAuthorizedTime { get; set; }
        public DateTime LastDeauthorizedTime { get; set; }

        private bool _noAutoAssignIps;
        public bool NoAutoAssignIps
        {
            get => _noAutoAssignIps;
            set
            {
                if (_noAutoAssignIps != value)
                {
                    _noAutoAssignIps = value;
                    OnPropertyChanged();
                }
            }
        }

        public required string Nwid { get; set; }
        public required string Objtype { get; set; }
        public int RemoteTraceLevel { get; set; }
        public string? RemoteTraceTarget { get; set; }
        public int Revision { get; set; }
        public List<object>? Tags { get; set; }
        public int VMajor { get; set; }
        public int VMinor { get; set; }
        public int VRev { get; set; }
        public int VProto { get; set; }

        private bool _ssoExempt;
        public bool SsoExempt
        {
            get => _ssoExempt;
            set
            {
                if (_ssoExempt != value)
                {
                    _ssoExempt = value;
                    OnPropertyChanged();
                }
            }
        }

        // public MemberConfigViewModel()
        // {
        //     _ipAssignment = string.Empty;
        // }


        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
