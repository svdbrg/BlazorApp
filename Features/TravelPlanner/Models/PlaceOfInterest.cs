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