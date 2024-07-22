using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class ItemInfo
    {
        public int ItemId { get; set; }

        public string? ItemName { get; set; }
        public string? ImageSource { get; set; }

        public string? addressCity { get; set; }

        public string? addressProvince { get; set; }

        public string? aboutMe { get; set; }

        public string? subjects { get; set; }

        public string? qualifications { get; set; }
        public string? availability { get; set; }

        public string? contactNumber { get; set; }

        // Convert byte[] to Base64 string
        private static string ConvertToBase64(byte[] imageBytes)
        {
            return $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
        }

    }
}
