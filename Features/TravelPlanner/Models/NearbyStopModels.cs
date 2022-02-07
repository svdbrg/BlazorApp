using Google.Cloud.Firestore;

namespace BlazorApp.Features.TravelPlanner.Models;

public class NearbyStopLocation
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<NearbyStop> NearbyStops { get; set; } = new List<NearbyStop>();
}

public class NearbyStop
{
    public string MainMastExtId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public override bool Equals(object? o)
    {
        var other = o as NearbyStop;
        return other?.Name == Name;
    }

    public override int GetHashCode() => Name?.GetHashCode() ?? 0;

    public override string ToString() => Name;
}

[FirestoreData]
public class NearbyStopDto
{
    [FirestoreDocumentId]
    public string Id { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Name { get; set; } = string.Empty;

    [FirestoreProperty]
    public string MainMastExtId { get; set; } = string.Empty;
}