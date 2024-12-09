# CC.Data

## Overview

This database system supports a web application designed to enhance video content accessibility for neurodivergent and audio-impaired individuals. It stores enriched contextual information, emotional cues, and detailed descriptions that go beyond traditional subtitles.

## Technology Stack

- Entity Framework Core 8.0
- PostgreSQL 14+
- .NET 8.0

## Setup Instructions

### 1. Prerequisites

- PostgreSQL 14 or higher
- .NET 8.0 SDK
- Entity Framework Core CLI tools

### 2. Connection String

Add the following connection string to your `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=video_context_db;Username=your_username;Password=your_password"
  }
}
```

### 3. Database Migration

Run the following commands in the project directory:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Entity Relationships

- One Video has many EnhancedCaptions (1:N)
- One EnhancedCaption has many Analysis (1:N)

## Performance Considerations

- Implemented pagination for caption retrieval
- Optimized queries for time-based caption searches
- Efficient storage of video metadata
- Caching strategies for frequently accessed content

## Data Access Examples

### Retrieving Enhanced Captions

```csharp
var captions = await context.EnhancedCaptions
    .Include(c => c.Analyses)
    .Where(c => c.VideoId == videoId)
    .OrderBy(c => c.StartTime)
    .ToListAsync();
```

### Adding New Video Content

```csharp
var video = new Video
{
    Title = "Sample Video",
    Description = "Description",
    VideoUrl = "url/to/video",
    Duration = TimeSpan.FromMinutes(5),
    CreatedAt = DateTime.UtcNow
};
context.Videos.Add(video);
await context.SaveChangesAsync();
```