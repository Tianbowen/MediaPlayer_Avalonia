using Avalonia;
using Avalonia.Media.Fonts;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using System;
using System.Diagnostics.CodeAnalysis;

namespace AvaloniaApplication1
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .UseReactiveUI()
                .UseAntDesignFontManager(); // 解决  Could not create glyphTypeface. Font family: $Default (key: ). Style: Normal. Weight: Normal. Stretch: Normal
    }

    public static class AvaloniaAppBuilderExtensions
    {
        /// <summary>
        /// 使用自定义字体
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configDelegate"></param>
        /// <returns></returns>
        public static AppBuilder UseAntDesignFontManager([DisallowNull] this AppBuilder builder, Action<FontSettings>? configDelegate = default)
        {
            var setting = new FontSettings();
            configDelegate?.Invoke(setting);

            //this setting can make app crash when publish for NativeAOT
            return builder.With(new FontManagerOptions
            {
                DefaultFamilyName = setting.DefaultFontFamily,
                FontFallbacks = new[]
                {
                new FontFallback
                {
                    FontFamily = new FontFamily(setting.DefaultFontFamily)
                }
            }

            }).ConfigureFonts(manager => manager.AddFontCollection(new EmbeddedFontCollection(setting.Key, setting.Source)));

        }
    }

    public class FontSettings
    {
        public string DefaultFontFamily = "fonts:AntDesignFontFamilies#Alibaba PuHuiTi 2.0";
        public Uri Key { get; set; } = new Uri("fonts:AntDesignFontFamilies", UriKind.Absolute);
        public Uri Source { get; set; } = new Uri("avares://AvaloniaApplication1/Assets/AliBaba", UriKind.Absolute);
    }
}
