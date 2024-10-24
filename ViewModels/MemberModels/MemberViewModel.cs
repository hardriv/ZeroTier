using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ZeroTier.ViewModels.MemberModels
{
    public class MemberViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public required string Id { get; set; }
        public required string Type { get; set; }
        public required DateTime Clock { get; set; }
        public required string NetworkId { get; set; }
        public required string NodeId { get; set; }
        public required string ControllerId { get; set; }
        public bool Hidden { get; set; }

        private string? _name;
        public string? Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _description;
        public string? Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private MemberConfigViewModel _config;
        public required MemberConfigViewModel Config
        {
            get => _config;
            set
            {
                if (_config != value)
                {
                    _config = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime LastOnline { get; set; }
        public DateTime LastSeen { get; set; }
        public string? PhysicalAddress { get; set; }
        public object? PhysicalLocation { get; set; }
        public string? ClientVersion { get; set; }
        public int ProtocolVersion { get; set; }
        public bool SupportsRulesEngine { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        // public MemberViewModel()
        // {
        //     _config = new MemberConfigViewModel();
        // }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
