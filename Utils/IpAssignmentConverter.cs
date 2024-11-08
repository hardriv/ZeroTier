using AutoMapper;
using System.Collections.Generic;
using ZeroTier.DTO.NetworkDtos;
using ZeroTier.ViewModels.NetworkModels;

namespace ZeroTier.Utils
{
    public class IpAssignmentConverter : ITypeConverter<List<IpAssignmentPoolDto>, IpAssignmentPoolViewModel>,
                                         ITypeConverter<IpAssignmentPoolViewModel, List<IpAssignmentPoolDto>>
    {
        public IpAssignmentPoolViewModel Convert(List<IpAssignmentPoolDto> source, IpAssignmentPoolViewModel destination, ResolutionContext context)
        {
            if ((source != null && source.Count > 0))
            {
                return new IpAssignmentPoolViewModel
                {
                    IpRangeStart = source[0].IpRangeStart,
                    IpRangeEnd = source[0].IpRangeEnd
                };
            }
            else
            {
                return new();
            }
        }

        public List<IpAssignmentPoolDto> Convert(IpAssignmentPoolViewModel source, List<IpAssignmentPoolDto> destination, ResolutionContext context)
        {
            if ((source != null && !string.IsNullOrEmpty(source.IpRangeStart) && !string.IsNullOrEmpty(source.IpRangeEnd)))
            {
                return [ new IpAssignmentPoolDto
                        {
                            IpRangeStart = source.IpRangeStart,
                            IpRangeEnd = source.IpRangeEnd
                        }];
            }
            else
            {
                return [];
            }
        }
    }
}