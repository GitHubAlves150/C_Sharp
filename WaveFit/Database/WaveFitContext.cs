using System.Data.Entity;
using WaveFit2.Database.Map;
using WaveFit2.Database.Model;

namespace WaveFit2.Database
{
    public class WaveFitContext : DbContext
    {
        public WaveFitContext() : base(Properties.Settings.Default.connectDB)
        {
        }

        public DbSet<AudioEvaluationModel> AudioEvaluation { get; set; }
        public DbSet<HearingAidModel> HearingAid { get; set; }
        public DbSet<HearingAidGainPlotModel> HearingAidGainPlot { get; set; }
        public DbSet<AudiogramModel> Audiogram { get; set; }
        public DbSet<CalibrationModel> Calibration { get; set; }
        public DbSet<FrequencyModel> Frequency { get; set; }
        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<SessionModel> Session { get; set; }
        public DbSet<LoginModel> Users { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<PlaceModel> Places { get; set; }
        public DbSet<AudiometerModel> Audiometers { get; set; }
        public DbSet<HealthCenterModel> HealthCenters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionModel>()
                        .HasRequired(f => f.Audiogram)
                        .WithMany(a => a.Session)
                        .HasForeignKey(f => f.IdAudiogram);

            modelBuilder.Entity<SessionModel>()
                        .HasRequired(f => f.AudioEvaluation)
                        .WithMany(a => a.Session)
                        .HasForeignKey(f => f.IdAudioEvaluation);

            modelBuilder.Entity<SessionModel>()
                        .HasRequired(a => a.Login)
                        .WithMany(p => p.Session)
                        .HasForeignKey(a => a.IdUser);

            modelBuilder.Entity<SessionModel>()
                        .HasRequired(a => a.Patient)
                        .WithMany(p => p.Session)
                        .HasForeignKey(a => a.IdPatient);

            modelBuilder.Entity<CalibrationModel>()
                        .HasRequired(a => a.Patient)
                        .WithMany(p => p.Calibration)
                        .HasForeignKey(a => a.IdPatient);

            modelBuilder.Entity<CalibrationModel>()
                        .HasRequired(a => a.HearingAid)
                        .WithMany(p => p.Calibration)
                        .HasForeignKey(a => a.IdHearingAid);

            modelBuilder.Entity<FrequencyModel>()
                        .HasRequired(f => f.Audiogram)
                        .WithMany(a => a.Frequency)
                        .HasForeignKey(f => f.IdAudiogram);

            modelBuilder.Entity<HearingAidGainPlotModel>()
                        .HasRequired(a => a.Calibration)
                        .WithMany(p => p.HearingAidGainPlot)
                        .HasForeignKey(a => a.IdCalibration);

            modelBuilder.Entity<PlaceModel>()
                        .HasRequired(a => a.Location)
                        .WithMany(p => p.Place)
                        .HasForeignKey(a => a.IdLocation);

            modelBuilder.Entity<HealthCenterModel>()
                        .HasRequired(a => a.Place)
                        .WithMany(p => p.HealthCenter)
                        .HasForeignKey(a => a.IdPlace);

            modelBuilder.Entity<HealthCenterModel>()
                        .HasRequired(a => a.Audiometer)
                        .WithMany(p => p.HealthCenter)
                        .HasForeignKey(a => a.IdAudiometer);

            modelBuilder.Entity<SessionModel>()
                        .HasRequired(a => a.HealthCenter)
                        .WithMany(p => p.Session)
                        .HasForeignKey(a => a.IdHealthCenter);

            modelBuilder.Configurations.Add(new AudioEvaluationMap());
            modelBuilder.Configurations.Add(new AudiogramMap());
            modelBuilder.Configurations.Add(new CalibrationMap());
            modelBuilder.Configurations.Add(new FrequencyMap());
            modelBuilder.Configurations.Add(new HearingAidMap());
            modelBuilder.Configurations.Add(new HearingAidGainPlotMap());
            modelBuilder.Configurations.Add(new LoginMap());
            modelBuilder.Configurations.Add(new PatientMap());
            modelBuilder.Configurations.Add(new SessionMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new PlaceMap());
            modelBuilder.Configurations.Add(new AudiometerMap());
            modelBuilder.Configurations.Add(new HealthCenterMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}