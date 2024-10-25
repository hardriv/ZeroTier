namespace ZeroTier.DTO.MemberDtos
{
    public class MemberUpdateDto
    {
        public bool Hidden { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public required MemberConfigUpdateDto Config { get; set; }
    }
}
