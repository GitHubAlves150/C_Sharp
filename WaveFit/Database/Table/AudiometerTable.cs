using System;
using System.Collections.Generic;
using WaveFit2.Database.Model;

namespace WaveFit2.Database.Table
{
    public class AudiometerTable
    {
        public static List<AudiometerModel> Audiometer()
        {
            return new List<AudiometerModel>
            {
                //SP Sala 1
                new AudiometerModel {
                                Code = "53477",
                                Model = "AMPLIVOX 270",
                                Maintenance = new DateTime(2023, 8, 4)},

                //SP Sala 2
                new AudiometerModel {
                                Code = "53387",
                                Model = "AMPLIVOX 270",
                                Maintenance = new DateTime(2023, 8, 4)},

                //FL Sala 1
                new AudiometerModel {
                                Code = "WST0087",
                                Model = "AMPLIVOX 270",
                                Maintenance = new DateTime(2023, 8, 2)},

                //FL Sala 2
                new AudiometerModel {
                                Code = "WST0082",
                                Model = "AMPLIVOX 270",
                                Maintenance = new DateTime(2023, 8, 2)}
            };
        }
    }
}