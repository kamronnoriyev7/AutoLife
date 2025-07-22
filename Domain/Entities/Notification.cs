using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public string? Title { get; set; } = default!;
    public string? Body { get; set; } = default!;
    public bool IsRead { get; set; } = false;

    public Guid ? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; } = null; // Agar bu bildirishnoma transport vositasiga tegishli bo'lsa

    public Guid ? ServiceCenterId { get; set; }
    public ServiceCenter? Service { get; set; } = null; // Agar bu bildirishnoma xizmatga tegishli bo'lsa


    public Guid? BookingId { get; set; } = null; // Agar bu bildirishnoma buyurtmaga tegishli bo'lsa
    public Booking? Booking { get; set; } = null; // Agar bu bildirishnoma buyurtmaga tegishli bo'lsa

    public Guid ? FuelStationId { get; set; } = null; // Agar bu bildirishnoma yoqilg'i quyish stantsiyasiga tegishli bo'lsa
    public FuelStation? FuelStation { get; set; } = null; // Agar bu bildirishnoma yoqilg'i quyish stantsiyasiga tegishli bo'lsa

    public Guid ? ParkingId { get; set; } = null; // Agar bu bildirishnoma avtoturargohga tegishli bo'lsa
    public Parking? Parking { get; set; } = null; // Agar bu bildirishnoma avtoturargohga tegishli bo'lsa

    public Guid ? CompanyId { get; set; } = null; // Agar bu bildirishnoma kompaniyaga tegishli bo'lsa
    public Company? Company { get; set; } = null; // Agar bu bildirishnoma kompaniyaga tegishli bo'lsa

}
