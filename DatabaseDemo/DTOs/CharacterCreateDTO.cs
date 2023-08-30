namespace DatabaseDemo.DTOs
{
    public record struct CharacterCreateDTO(string Name, 
        BackpackCreateDto Backpack, 
        List<WeaponCreateDto> Weapons);
}
