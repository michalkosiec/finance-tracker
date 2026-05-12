using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Stats
{
    public class MonthlyStatsReadDto {
        [Required]
        public List<MonthlyStatReadDto> Months = new List<MonthlyStatReadDto>();
    }
}