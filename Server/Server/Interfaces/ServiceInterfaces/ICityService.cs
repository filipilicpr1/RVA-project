﻿using Server.Dto.CityDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface ICityService
    {
        Task<List<DisplayCityDTO>> GetAllDistinct();
        Task<List<DisplayCityDTO>> GetAvailable(int id);
        Task<DisplayCityDTO> CreateCity(NewCityDTO newCityDTO);
        Task DeleteCity(int id);
    }
}