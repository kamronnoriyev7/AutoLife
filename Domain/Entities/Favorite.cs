using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;


public class Favorite : BaseEntity
{
    public long Id { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public FavoriteType Type { get; set; }   // Nimani yoqtirdi: Station, Parking, Vehicle, Service]

    public long? FuelStationId { get; set; }       // Qaysi IDdagi objectni yoqtirdi
    public FuelStation? FuelStation { get; set; } = null; // Agar bu yoqtirish yoqilg'i quyish stantsiyasiga tegishli bo'lsa

    public long? ParkingId { get; set; }           // Qaysi IDdagi objectni yoqtirdi
    public Parking? Parking { get; set; } = null; // Agar bu yoqtirish avtoturargohga tegishli bo'lsa

    public long? VehicleId { get; set; }           // Qaysi IDdagi objectni yoqtirdi
    public Vehicle? Vehicle { get; set; } = null; // Agar bu yoqtirish transport vositasiga tegishli bo'lsa

    public long? ServiceCenterId { get; set; } // Qaysi IDdagi objectni yoqtirdi
    public ServiceCenter? ServiceCenter { get; set;} = null; // Agar bu yoqtirish xizmat ko'rsatish markaziga tegishli bo'lsa
}
