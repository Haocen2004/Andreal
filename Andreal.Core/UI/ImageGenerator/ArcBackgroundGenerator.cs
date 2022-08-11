﻿using System.Drawing;
using Andreal.Core.Model.Arcaea;
using Andreal.Core.UI.Model;
using Path = Andreal.Core.Common.Path;

namespace Andreal.Core.UI.ImageGenerator;

#pragma warning disable CA1416

internal class ArcBackgroundGenerator
{
    private readonly ArcaeaChart _info;

    public ArcBackgroundGenerator(RecordInfo recordInfo) { _info = recordInfo.SongInfo; }

    internal async Task<BackGround> ArcV1()
    {
        var path = Path.ArcaeaBackground(1, _info);
        return path.FileInfo.Exists
            ? new(path)
            : await GenerateArcV1(path);
    }

    private async Task<BackGround> GenerateArcV1(Path path)
    {
        using var song = await _info.GetSongImage();
        using var temp = song.Cut(new(0, 87, 512, 341));
        var background = new BackGround(temp, 1440, 960);
        using var masktmp = background.Blur(20).Cut(new(50, 50, 1340, 860)).Blur(80);
        background.FillColor(song.MainColor);
        background.Draw(new ImageModel(Path.ArcaeaBg1Mask, 0, 0, 1440, 960), new ImageModel(masktmp, 50, 50, 1340, 860),
                        new ImageModel(Path.ArcaeaDivider, 100, 840, 1240),
                        new TextWithStrokeModel("Pure", Font.Exo40, Color.White, 518, 488),
                        new TextWithStrokeModel("Far", Font.Exo40, Color.White, 518, 553),
                        new TextWithStrokeModel("Lost", Font.Exo40, Color.White, 518, 618),
                        new TextWithStrokeModel("PTT", Font.Exo40, Color.White, 518, 683),
                        new TextWithStrokeModel("Generated by Project Andreal", Font.KazesawaLight24, Color.White, 80,
                                                865));
        background.SaveAsPng(path);
        return background;
    }

    internal async Task<BackGround> ArcV2()
    {
        var path = Path.ArcaeaBackground(2, _info);
        return path.FileInfo.Exists
            ? new(path)
            : await GenerateArcV2(path);
    }

    private async Task<BackGround> GenerateArcV2(Path path)
    {
        using var song = await _info.GetSongImage();
        using var temp = song.Cut(new(0, 112, 512, 288));
        var background = new BackGround(temp, 1920, 1080).Blur(60);
        background.FillColor(song.MainColor);
        background.Draw(new TextWithShadowModel("Play PTT", Font.Exo36, 123, 355),
                        new TextWithShadowModel("Pure", Font.Exo32, 127, 455),
                        new TextWithShadowModel("Far", Font.Exo32, 127, 525),
                        new TextWithShadowModel("Lost", Font.Exo32, 410, 525),
                        new TextWithShadowModel("Played at", Font.Exo32, 127, 595),
                        new ImageModel(new BackGround(song.Cut(new(0, 19, 512, 48)), 1920, 180).Blur(20), 0, 740),
                        new LineModel(Color.White, 3, new(0, 740), new(1920, 740)),
                        new LineModel(Color.White, 3, new(0, 920), new(1920, 920)),
                        new LineModel(Color.White, 1, new(0, 705), new(1920, 705)),
                        new LineModel(Color.White, 1, new(0, 955), new(1920, 955)),
                        new RectangleModel(Color.GetBySide(_info.Side), new(145, 685, 320, 320)),
                        new ImageModel(song, 130, 670, 320, 320),
                        new TextWithShadowModel(_info.GetSongName(50), Font.Andrea56, 510, 750));
        background.SaveAsPng(path);
        return background;
    }

    internal async Task<BackGround> ArcV3()
    {
        var path = Path.ArcaeaBackground(3, _info);
        return path.FileInfo.Exists
            ? new(path)
            : await GenerateArcV3(path);
    }

    private async Task<BackGround> GenerateArcV3(Path path)
    {
        using var song = await _info.GetSongImage();
        using var temp = song.Cut(new(78, 0, 354, 512));
        var background = new BackGround(temp, 1000, 1444).Blur(10);
        background.FillColor(Color.White, 100);
        background.Draw(new ImageModel(Path.ArcaeaBg3Mask(_info.Side), 0, 0, 1000),
                        new TextOnlyModel(_info.GetSongName(30), Font.Beatrice36, Color.Black, 500, 860,
                                          StringAlignment.Center), new ImageModel(song, 286, 408, 428),
                        new TextOnlyModel("PlayPtt:", Font.Exo24, Color.GnaqGray, 110, 1275),
                        new TextOnlyModel("PlayTime:", Font.Exo24, Color.GnaqGray, 110, 1355),
                        new TextOnlyModel("Pure", Font.Exo24, Color.Black, 635, 1260),
                        new TextOnlyModel("Far", Font.Exo24, Color.Black, 635, 1315),
                        new TextOnlyModel("Lost", Font.Exo24, Color.Black, 635, 1370));
        background.SaveAsPng(path);
        return background;
    }
}
