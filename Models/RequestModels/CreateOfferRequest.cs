using RentApp.Models.DbModels;
using RentApp.Models.Interfaces;
using RentApp.Models.Structs;
using RentApp.Utilities;
using System;
using System.Collections.Generic;

namespace RentApp.Models.RequestModels
{
    public class CreateOfferRequest : IOffer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public OfferType OfferType { get; set; }
        public PropertyType PropertyType { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int Price { get; set; }
        public List<string> PhotoURLs { get; set; } = new List<string>();
        public int? RoomsQuantity { get; set; }
        public int? FloorNumber { get; set; }
        public double? Area { get; set; }
        public int? Payments { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTill { get; set; }
        public bool? WithFurniture { get; set; }
        public bool? WithBalcony { get; set; }
        public bool? WithParking { get; set; }
        public bool? AllowPets { get; set; }
        public bool? AllowChildren { get; set; }
        public string Description { get; set; }

        private List<PropertyPhoto> _propertyPhotos;
        public List<PropertyPhoto> PropertyPhotos {
            get
            {
                if(_propertyPhotos==null)
                {
                    _propertyPhotos = new List<PropertyPhoto>();
                    var imageUtility = new ImageUtility(PhotoType.Property);
                    int orderNumber = 0;
                    PhotoURLs.ForEach(f =>
                    {
                        var id = (imageUtility.UpdateImageId(null, f));
                        _propertyPhotos.Add(new PropertyPhoto()
                        {
                            Id = id.Value,
                            OfferId = Id,
                            Base64 = f,
                            Url = imageUtility.GetUploadedImageUrl(id.Value),
                            OrderNumber = ++orderNumber
                        });
                    });
                }
                return _propertyPhotos;
            }
            set {
                _propertyPhotos = value;
            }
        }
    }
}
