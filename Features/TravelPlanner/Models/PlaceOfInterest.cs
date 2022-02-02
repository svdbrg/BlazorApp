using Google.Cloud.Firestore;

namespace BlazorApp.Features.TravelPlanner.Models;

public class PlaceOfInterest
{
    public string Name { get; set; } = string.Empty;
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}

[FirestoreData]
public class PlaceOfInterestDto
{
    [FirestoreDocumentId]
    public string Id { get; set; } = string.Empty;
    
    [FirestoreProperty]
    public string Name { get; set; } = string.Empty;
    
    [FirestoreProperty]
    public double Longitude { get; set; }
    
    [FirestoreProperty]
    public double Latitude { get; set; }
}

public static class Places
{
    public static IEnumerable<PlaceOfInterest> PlacesOfInterest = new List<PlaceOfInterest> {
                new PlaceOfInterest { Name="L86", Latitude = 59.3335035, Longitude = 18.0148875 },
                new PlaceOfInterest { Name="Floragatan 13", Latitude = 59.3428308, Longitude = 18.0755565 }
            };
    public static PlaceOfInterest Home = new PlaceOfInterest
    {
        Name = "Solstigen",
        Latitude = 59.32315313171132,
        Longitude = 18.20933732771809
    };
}