namespace Vitems.Util;

public class Style
{
    public static string Damage(string text)
    {
        return $"<style=cIsDamage>{text}</style>";
    }

    public static string Utility(string text)
    {
        return $"<style=cIsUtility>{text}</style>";
    }

    public static string Healing(string text)
    {
        return $"<style=cIsHealing>{text}</style>";
    }

    public static string Void(string text)
    {
        return $"<style=cIsVoid>{text}</style>";
    }

    public static string Stack(string text)
    {
        return $"<style=cStack>{text}</style>";
    }
}
