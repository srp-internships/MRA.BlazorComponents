using System.Globalization;

namespace MRA.BlazorComponents;

public static class Environmets
{
    public static CultureInfo ApplicationCulture = new(ApplicationCulturesNames.Ru);
}

public static class ApplicationCulturesNames
{
    public const string En = "en-US";
    public const string Ru = "ru-RU";
    public const string Tj = "tj";
}