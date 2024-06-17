using Dotmim.Sync;
using Dotmim.Sync.PostgreSql;
using HandyControl.Controls;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WaveFit2.Audiogram.ViewModel;
using WaveFit2.Database.Model;
using WaveFit2.Database.Table;

namespace WaveFit2.Database.CRUD
{
    public class Crud
    {
        private readonly WaveFitContext _context = new WaveFitContext();

        public DataSet dt = new DataSet();

        public bool isConnectionErrorMessageShown = false;

        public async void SyncData()
        {
            try
            {
                var serverProvider = new NpgsqlSyncProvider("Server=34.23.83.77;port=5432;Database=GlobalDb;User Id=postgres;Password=admin");
                var clientProvider = new NpgsqlSyncProvider("Server=localhost;port=5432;Database=LocalDb;User Id=postgres;Password=admin");

                var setup = new SyncSetup("dbo.audiogram", "dbo.frequency", "dbo.patients", "dbo.session", "dbo.users");

                var agent = new SyncAgent(clientProvider, serverProvider);

                var result = await agent.SynchronizeAsync(setup);

                //MessageBox.Show(result.ToString());
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
        }

        #region User

        public void AddUser(LoginModel userModels)
        {
            try
            {
                if (!_context.Users.Any(u => u.Id == userModels.Id))
                {
                    _context.Users.Add(userModels);
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetUserById(int userId)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = new NpgsqlCommand($"SELECT id, name, surname, username, crfa, image, permission " +
                                                    $"FROM dbo.users " +
                                                    $"WHERE id = @UserId", connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Properties.Settings.Default.userId = reader.GetInt32(0);
                        Properties.Settings.Default.userName = reader.GetString(1);
                        Properties.Settings.Default.userSurname = reader.GetString(2);
                        Properties.Settings.Default.userUsername = reader.GetString(3);
                        Properties.Settings.Default.userCRM = reader.GetString(4);
                        Properties.Settings.Default.userPermission = reader.GetInt16(6);

                        if (!reader.IsDBNull(5))
                        {
                            long len = reader.GetBytes(5, 0, null, 0, 0);
                            byte[] buffer = new byte[len];
                            reader.GetBytes(5, 0, buffer, 0, (int)len);
                            Properties.Settings.Default.imageUser = Convert.ToBase64String(buffer);
                        }
                        else
                        {
                            Properties.Settings.Default.imageUser = null;
                        }
                    }
                    reader.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion User

        #region Patient

        public void AddPatient(List<PatientModel> patientModels)
        {
            try
            {
                var patient = new PatientModel();
                foreach (var user in patientModels)
                {
                    if (!_context.Patients.Any(u => u.Id == user.Id) && !_context.Patients.Any(u => u.PatientCode == user.PatientCode))
                    {
                        _context.Patients.Add(user);
                        patient = user;
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool CheckPatientCode(List<PatientModel> patientModels)
        {
            bool check = true;
            var patient = new PatientModel();
            foreach (var user in patientModels)
            {
                if (_context.Patients.Any(u => u.PatientCode == user.PatientCode))
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }
            return check;
        }

        public void GetPatientId(long code)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"SELECT id " +
                                          $"FROM dbo.patients " +
                                          $"WHERE patientcode=@CODE";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@CODE", code);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Properties.Settings.Default.patientId = dr.GetInt32(0);
                    }
                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public List<PatientModel> GetPatient(long code)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"SELECT name, surname, gender, birthday, typedoc, numdoc " +
                                          $"FROM dbo.patients " +
                                          $"WHERE patientcode=@CODE";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@CODE", code);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    List<PatientModel> prints = new List<PatientModel>();

                    while (dr.Read())
                    {
                        PatientModel print = new PatientModel
                        {
                            Name = dr.GetString(0),
                            Surname = dr.GetString(1),
                            Gender = dr.GetString(2),
                            Birthday = dr.GetDateTime(3),
                            TypeDocument = dr.GetString(4),
                            NumDocument = dr.GetString(5)
                        };
                        prints.Add(print);
                    }
                    dr.Close();

                    return prints;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return null; // Retorne null em caso de erro ou se nenhum paciente for encontrado
        }

        public void GetIdAudiogram(DateTime date)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = "SELECT id FROM dbo.audiogram WHERE date = @date";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("date", date);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Properties.Settings.Default.audiogramId = dr.GetInt32(0);
                    }
                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public List<AudioEvaluationModel> GetAudioEvaluation(int id)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"SELECT *" +
                                          $"FROM dbo.audioevaluation " +
                                          $"WHERE id=@ID";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@ID", id);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    List<AudioEvaluationModel> prints = new List<AudioEvaluationModel>();

                    while (dr.Read())
                    {
                        AudioEvaluationModel print = new AudioEvaluationModel
                        {
                            Id = dr.GetInt32(0),

                            LeftMeatoscopy = dr.GetString(1),
                            RightMeatoscopy = dr.GetString(2),

                            LeftEarLRF = dr.GetString(3),
                            RightEarLRF = dr.GetString(4),
                            LeftEarLAF = dr.GetString(5),
                            RightEarLAF = dr.GetString(6),
                            LeftEarIntesity = dr.GetString(7),
                            RightEarIntesity = dr.GetString(8),
                            WordsMono = dr.GetString(9),
                            WordsDi = dr.GetString(10),
                            WordsTri = dr.GetString(11),
                            OEMono = dr.GetString(12),
                            OEDi = dr.GetString(13),
                            OETri = dr.GetString(14),
                            ODMono = dr.GetString(15),
                            ODDi = dr.GetString(16),
                            ODTri = dr.GetString(17),

                            LeftEarVAMin = dr.GetString(18),
                            LeftEarVAMax = dr.GetString(19),
                            LeftEarVOMin = dr.GetString(20),
                            LeftEarVOMax = dr.GetString(21),
                            LeftEarLogo = dr.GetString(22),
                            RightEarVAMin = dr.GetString(23),
                            RightEarVAMax = dr.GetString(24),
                            RightEarVOMin = dr.GetString(25),
                            RightEarVOMax = dr.GetString(26),
                            RightEarLogo = dr.GetString(27),

                            Obs = dr.GetString(28)
                        };
                        prints.Add(print);
                    }
                    dr.Close();

                    return prints;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return null; // Retorne null em caso de erro ou se nenhum paciente for encontrado
        }

        public List<LoginModel> LoadUsersDataFromDatabase(int idToExclude)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdSelect = "SELECT name, surname, username, crfa, enabled, permission FROM dbo.users WHERE id != @idToExclude";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdSelect, connection);
                    cmd.Parameters.AddWithValue("@idToExclude", idToExclude);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    List<LoginModel> users = new List<LoginModel>();

                    while (dr.Read())
                    {
                        LoginModel item = new LoginModel
                        {
                            Name = dr.GetString(0),
                            Surname = dr.GetString(1),
                            Username = dr.GetString(2),
                            CRFa = dr.GetString(3),
                            Enabled = dr.GetBoolean(4),
                            Permission = dr.GetInt16(5),
                        };

                        users.Add(item);
                    }

                    dr.Close();

                    return users;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<PatientModel> LoadPatientDataFromDatabase(bool status)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdSelect = "SELECT patientcode, name, surname, gender, birthday, typedoc, numdoc FROM dbo.patients WHERE status = @status";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdSelect, connection);
                    cmd.Parameters.AddWithValue("status", status);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    List<PatientModel> patients = new List<PatientModel>();

                    while (dr.Read())
                    {
                        PatientModel patient = new PatientModel
                        {
                            PatientCode = dr.GetInt64(0),
                            Name = dr.GetString(1),
                            Surname = dr.GetString(2),
                            Gender = dr.GetString(3),
                            Birthday = dr.GetDateTime(4).Date,
                            TypeDocument = dr.GetString(5),
                            NumDocument = dr.GetString(6)
                        };

                        patients.Add(patient);
                    }

                    dr.Close();

                    return patients;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<AudiogramModel> GetAudiogramDate(int audiogramId)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<AudiogramModel> audiograms = new List<AudiogramModel>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdText = "SELECT id, date FROM dbo.audiogram WHERE id = @audiogramId";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdText, connection);
                    cmd.Parameters.AddWithValue("audiogramId", audiogramId);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        AudiogramModel audiogram = new AudiogramModel()
                        {
                            Id = dr.GetInt32(0),
                            Date = dr.GetDateTime(1)
                        };
                        audiograms.Add(audiogram);
                    }
                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return audiograms;
        }

        public List<AudiogramModel> LoadAudiogramDataFromDatabase(int idPatient)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = "SELECT a.id, a.date " +
                                            "FROM dbo.audiogram a " +
                                            "INNER JOIN dbo.session s ON s.idaudiogram = a.id " +
                                            "WHERE s.idpatient = @idPatient";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("idPatient", idPatient);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    List<AudiogramModel> audiograms = new List<AudiogramModel>();

                    while (dr.Read())
                    {
                        AudiogramModel audiogram = new AudiogramModel()
                        {
                            Id = dr.GetInt32(0),
                            Date = dr.GetDateTime(1)
                        };

                        audiograms.Add(audiogram);
                    }
                    dr.Close();
                    return audiograms;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<CalibrationModel> LoadFittingDataFromDatabase(int idPatient, int idDevice, string channel, DateTime date)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdFitting = $"SELECT a.id, a.program, a.paramters, a.configuration FROM dbo.fitting a INNER JOIN dbo.hearingaid s ON s.id = a.idhearingaid WHERE a.idpatient = @idPatient AND a.idhearingaid = @idDevice AND a.channel = @channel AND a.date = @date ORDER BY a.program";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdFitting, connection);
                    cmd.Parameters.AddWithValue("idPatient", idPatient);
                    cmd.Parameters.AddWithValue("idDevice", idDevice);
                    cmd.Parameters.AddWithValue("channel", channel);
                    cmd.Parameters.AddWithValue("date", date);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    List<CalibrationModel> fittings = new List<CalibrationModel>();

                    while (dr.Read())
                    {
                        CalibrationModel fitting = new CalibrationModel()
                        {
                            Id = dr.GetInt32(0),
                            Program = dr.GetInt32(1) + 1,
                            Params = dr.GetString(2),
                            Config = dr.GetString(3)
                        };

                        fittings.Add(fitting);
                    }
                    dr.Close();
                    return fittings;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdatePatient(long patientcode, string name, string surname, string gender, DateTime birthday, string type, string doc)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdUpdate = "UPDATE dbo.patients " +
                                       "SET name = @Name, surname = @Surname, gender = @Gender, birthday = @Birthday, typedoc = @Type, numdoc = @Doc  " +
                                       "WHERE patientcode = @PatientCode";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(cmdUpdate, connection))
                    {
                        cmd.Parameters.AddWithValue("@PatientCode", patientcode);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Surname", surname);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Birthday", birthday);
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Doc", doc);

                        int rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateStatePatient(long patientCode, bool statePatient)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = "UPDATE dbo.patients " +
                                          "SET Status = @StatePatient " +
                                          "WHERE PatientCode = @PatientCode";

                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@PatientCode", patientCode);
                    cmd.Parameters.AddWithValue("@StatePatient", statePatient);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion Patient

        #region Audiogram

        public AudiographModel GetAudiograph(int idAudiogram)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdSelect = "SELECT * " +
                                       "FROM dbo.frequency " +
                                       "WHERE idaudiogram = @id " +
                                       "ORDER BY id";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdSelect, connection);
                    cmd.Parameters.AddWithValue("id", idAudiogram);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    int index = 0;
                    AudiographModel audiographModel = new AudiographModel();

                    while (dr.Read())
                    {
                        audiographModel.Type[index] = dr.GetString(2);
                        audiographModel.Ear[index] = dr.GetChar(3);

                        for (int i = 4; i < 15; i++)
                        {
                            string[] substrings = dr.GetString(i).Split('&');
                            audiographModel.Intensity[index, i - 4] = double.Parse(substrings[0]);
                            audiographModel.Marker[index, i - 4] = int.Parse(substrings[1]);
                        }
                        index++;
                    }

                    dr.Close();

                    return audiographModel;
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public void AddFrequencies(List<FrequencyModel> frequencyModels)
        {
            try
            {
                var frequencies = new FrequencyModel();
                foreach (var frequency in frequencyModels)
                {
                    if (!_context.Frequency.Any(u => u.Id == frequency.Id))
                    {
                        _context.Frequency.Add(frequency);
                        frequencies = frequency;
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddSession(List<SessionModel> sessionModels)
        {
            try
            {
                var sessions = new SessionModel();
                foreach (var session in sessionModels)
                {
                    if (!_context.Session.Any(u => u.Id == session.Id))
                    {
                        _context.Session.Add(session);
                        sessions = session;
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddAudiogram(List<AudiogramModel> audiogramModels)
        {
            try
            {
                var audiograms = new AudiogramModel();
                foreach (var audiogram in audiogramModels)
                {
                    if (!_context.Audiogram.Any(u => u.Id == audiogram.Id))
                    {
                        _context.Audiogram.Add(audiogram);
                        audiograms = audiogram;
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddAudioEvaluation(List<AudioEvaluationModel> audioEvalutaionsModels)
        {
            try
            {
                var evaluation = new AudioEvaluationModel();
                foreach (var audioEvalutaion in audioEvalutaionsModels)
                {
                    if (!_context.AudioEvaluation.Any(u => u.Id == audioEvalutaion.Id))
                    {
                        _context.AudioEvaluation.Add(audioEvalutaion);
                        evaluation = audioEvalutaion;
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion Audiogram

        #region Calibration

        public void AddHearingAid(HearingAidModel hearingAidModel)
        {
            try
            {
                if (!_context.HearingAid.Any(u => u.SerialNumber == hearingAidModel.SerialNumber || u.Device == hearingAidModel.Device))
                {
                    _context.HearingAid.Add(hearingAidModel);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void UpdateCalibrationParamsAndConfig(int calibrationId, string paramsValue, string configValue)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = "UPDATE dbo.fitting" +
                                   "SET paramters = @Params, configuration = @Config" +
                                   "WHERE id = @Id";

                    NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Params", paramsValue);
                    cmd.Parameters.AddWithValue("@Config", configValue);
                    cmd.Parameters.AddWithValue("@Id", calibrationId);

                    int affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        MessageBox.Show("Nenhuma calibração foi atualizada. Verifique se o ID é válido.");
                    }
                    else
                    {
                        MessageBox.Show("Calibração atualizada com sucesso.");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Erro ao atualizar calibração: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao executar a operação: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void AddCalibration(List<CalibrationModel> calibrationModels)
        {
            try
            {
                var calibration = new CalibrationModel();
                foreach (var calibrationModel in calibrationModels)
                {
                    if (!_context.Calibration.Any(u => u.Id == calibrationModel.Id))
                    {
                        _context.Calibration.Add(calibrationModel);
                        calibration = calibrationModel;
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void AddHearingAidGainPLot(List<HearingAidGainPlotModel> hearingAidGainPLotModels)
        {
            try
            {
                foreach (var hearingAidGainPLotModel in hearingAidGainPLotModels)
                {
                    if (!_context.HearingAidGainPlot.Any(u => u.Id == hearingAidGainPLotModel.Id))
                    {
                        _context.HearingAidGainPlot.Add(hearingAidGainPLotModel);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public List<CalibrationModel> GetCalibrationsByDate(DateTime date)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<CalibrationModel> calibrations = new List<CalibrationModel>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = "SELECT * FROM dbo.fitting WHERE date=@Date";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("Date", date);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CalibrationModel calibration = new CalibrationModel
                            {
                                Id = reader.GetInt32(0),
                                IdPatient = reader.GetInt32(1),
                                IdHearingAid = reader.GetInt32(2),
                                Channel = reader.GetString(3),
                                Program = reader.GetInt32(4),
                                Params = reader.GetString(5),
                                Config = reader.GetString(6),
                                Date = reader.GetDateTime(7)
                            };
                            calibrations.Add(calibration);
                        }
                    }
                }
                return calibrations;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public List<HearingAidModel> GetHearingAidBySN(long SN)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<HearingAidModel> hearingAids = new List<HearingAidModel>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = "SELECT * FROM dbo.hearingaid WHERE serialnumber=@SN";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("SN", SN);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HearingAidModel hearingAid = new HearingAidModel
                            {
                                Id = reader.GetInt32(0),
                                SerialNumber = reader.GetInt64(1),
                                Device = reader.GetString(2),
                                Receptor = reader.GetString(3),
                            };
                            hearingAids.Add(hearingAid);
                        }
                    }
                }
                return hearingAids;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public List<HearingAidGainPlotModel> GetHearingAidGainPlotsByFitting(int fittingid)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<HearingAidGainPlotModel> gainPlots = new List<HearingAidGainPlotModel>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = "SELECT * FROM dbo.fitting WHERE idfitting=@IdFitting";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("IdFitting", fittingid);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HearingAidGainPlotModel gainPlot = new HearingAidGainPlotModel
                            {
                                Id = reader.GetInt32(0),
                                IdCalibration = reader.GetInt32(1),
                            };
                            gainPlots.Add(gainPlot);
                        }
                    }
                }
                return gainPlots;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        #endregion Calibration

        #region Records

        public List<DateTime> GetRecordDates(int idPatient, int idHearingAid, string channel)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<DateTime> resultList = new List<DateTime>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT DISTINCT date, MAX(id) FROM dbo.fitting WHERE idpatient = @value AND idhearingaid = @value2 AND channel = @value3 GROUP BY date ORDER BY MAX(id) DESC, date";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", idPatient);
                    cmd.Parameters.AddWithValue("value2", idHearingAid);
                    cmd.Parameters.AddWithValue("value3", channel);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList.Add(dr.GetDateTime(0).Date);
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public DateTime GetRecordDate(int idPatient, int idHearingAid, string channel)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            DateTime resultList = new DateTime();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT DISTINCT date, MAX(id) FROM dbo.fitting WHERE idpatient = @value AND idhearingaid = @value2 AND channel = @value3 GROUP BY date ORDER BY MAX(id) ASC, date";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", idPatient);
                    cmd.Parameters.AddWithValue("value2", idHearingAid);
                    cmd.Parameters.AddWithValue("value3", channel);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList = dr.GetDateTime(0).Date;
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        #endregion

        #region General

        public void UpdateColBool(string table, string col, bool value, int id)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"UPDATE {table} SET {col}=@Value WHERE id=@Id";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Value", value);
                    cmd.ExecuteNonQuery();
                    _context.SaveChanges();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateColBoolUserName(string table, string col, bool value, string username)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"UPDATE {table} SET {col}=@Value WHERE username=@Username";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Value", value);
                    cmd.ExecuteNonQuery();
                    _context.SaveChanges();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateColInt(string table, int value, int id, string col)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"UPDATE {table} SET {col}=@Value WHERE id=@Id";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Value", value);
                    cmd.ExecuteNonQuery();
                    _context.SaveChanges();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateColIntByUserName(string table, int value, string username, string col)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"UPDATE {table} SET {col}=@Value WHERE username=@UserName";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@Value", value);
                    cmd.ExecuteNonQuery();
                    _context.SaveChanges();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateColDouble(string table, double value, int id, string col)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"UPDATE {table} SET \"{col}\"=@Value WHERE id=@Id";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Value", value);
                    cmd.ExecuteNonQuery();
                    _context.SaveChanges();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateColDate(string table, DateTime value, int id, string col)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"UPDATE {table} SET \"{col}\"=@Value WHERE id=@Id";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Value", value);
                    cmd.ExecuteNonQuery();
                    _context.SaveChanges();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateColString(string table, string value, int id, string col)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"UPDATE {table} SET {col}=@Value WHERE id=@Id";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Value", value);
                    cmd.ExecuteNonQuery();
                    _context.SaveChanges();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show("Inner Exception: " + ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show("Inner Exception: " + ex.InnerException.Message);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateColImage(string table, byte[] value, int id, string col)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"UPDATE {table} SET {col}=@Value WHERE id=@Id";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Value", value);
                    cmd.ExecuteNonQuery();
                    _context.SaveChanges();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool GetAtributeBool(string atribute, string table, string col, int value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    bool At = false;

                    while (dr.Read())
                    {
                        At = dr.GetBoolean(0);
                    }

                    dr.Close();
                    return At;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return false;
        }

        public bool GetAtributeBool(string atribute, string table, string col, string value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    bool At = false;

                    while (dr.Read())
                    {
                        At = dr.GetBoolean(0);
                    }

                    dr.Close();
                    return At;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return false;
        }

        public List<bool> GetAtributeListBool(string atribute, string table, string col, int value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<bool> resultList = new List<bool>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList.Add(dr.GetBoolean(0));
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public List<bool> GetAtributeListBool(string atribute, string table, string col, string value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<bool> resultList = new List<bool>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} " +
                                            $"FROM {table} " +
                                            $"WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList.Add(dr.GetBoolean(0));
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public int GetAtributeInt(string atribute, string table, string col, int value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    int At = 0;

                    while (dr.Read())
                    {
                        At = dr.GetInt32(0);
                    }

                    dr.Close();
                    return At;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return 0;
        }

        public int GetAtributeInt(string atribute, string table, string col, long value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    int At = 0;

                    while (dr.Read())
                    {
                        At = dr.GetInt32(0);
                    }

                    dr.Close();
                    return At;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return 0;
        }

        public int GetAtributeInt(string atribute, string table, string col, string value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    int At = 0;

                    while (dr.Read())
                    {
                        At = dr.GetInt32(0);
                    }

                    dr.Close();
                    return At;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return 0;
        }

        public int GetAtributeInt(string atribute, string table, string col, DateTime value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    int At = 0;

                    while (dr.Read())
                    {
                        At = dr.GetInt32(0);
                    }

                    dr.Close();
                    return At;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return 0;
        }

        public List<int> GetAtributeListInt(string atribute, string table, string col, int value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<int> resultList = new List<int>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT DISTINCT {atribute}, MAX(id) FROM {table} WHERE {col} = @value GROUP BY {atribute} ORDER BY MAX(id) DESC, {atribute}";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList.Add(dr.GetInt32(0));
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public List<int> GetAtributeListIntById(string atribute, string table, string col, int value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<int> resultList = new List<int>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT DISTINCT {atribute}, id FROM {table} WHERE {col} = @value ORDER BY id";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList.Add(dr.GetInt32(0));
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public List<int> GetAtributeListInt(string atribute, string table, string col, string value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<int> resultList = new List<int>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT DISTINCT {atribute} " +
                                            $"FROM {table} " +
                                            $"WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList.Add(dr.GetInt32(0));
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public List<int> GetAtributeListInt(string atribute, string table, string col, string value, string col2, bool value2)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<int> resultList = new List<int>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT DISTINCT {atribute}, id FROM {table} WHERE {col} = @value AND {col2} = @value2 ORDER BY id";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    cmd.Parameters.AddWithValue("value2", value2);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList.Add(dr.GetInt32(0));
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public long GetAtributeLong(string atribute, string table, string col, long value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            long At = 0;

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        At = dr.GetInt64(0);
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return At;
        }

        public string GetAtributeString(string atribute, string table, string col, int value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            string At = "";

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        At = dr.GetString(0);
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return At;
        }

        public string GetAtributeString(string atribute, string table, string col, long value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            string At = "";

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        At = dr.GetString(0);
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return At;
        }

        public List<string> GetAtributeStrings(string atribute, string table, string col, int value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<string> resultList = new List<string>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT DISTINCT {atribute}, id FROM {table} WHERE {col} = @value ORDER BY id";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList.Add(dr.GetString(0));
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public List<string> GetAtributeStrings(string atribute, string table, string col, bool value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<string> resultList = new List<string>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT DISTINCT {atribute}, id " +
                                            $"FROM {table} " +
                                            $"WHERE {col} = @value " +
                                            $"ORDER BY id";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList.Add(dr.GetString(0));
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public List<string> GetAtributeStrings(string atribute, string table)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<string> resultList = new List<string>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT DISTINCT {atribute}, id FROM {table} ORDER BY id";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList.Add(dr.GetString(0));
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public string[] GetAtributeStringArray(string atribute, string table, string col, int value)
        {
            var conexao = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    string cmdHI = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdHI, conexao);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    string[] substring = new string[] { };

                    while (dr.Read())
                    {
                        substring = dr.GetString(0).Split('&');
                    }

                    dr.Close();
                    return substring;
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Close();
            }
        }

        public string GetGender(int patientId)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdGetGender = "SELECT gender FROM dbo.patients WHERE id = @patientId";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdGetGender, connection);
                    cmd.Parameters.AddWithValue("patientId", patientId);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    string gender = string.Empty;

                    while (dr.Read())
                    {
                        gender = dr.GetString(0);
                    }

                    dr.Close();
                    return gender;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return string.Empty;
        }

        public DateTime GetAtributeDate(string atribute, string table, string col, int value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} " +
                                            $"FROM {table} " +
                                            $"WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    DateTime At = DateTime.Now;

                    while (dr.Read())
                    {
                        At = dr.GetDateTime(0);
                    }

                    dr.Close();
                    return At;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return DateTime.Now;
        }

        public List<DateTime> GetAtributeListDate(string atribute, string table, string col, int value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            List<DateTime> resultList = new List<DateTime>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute}, id FROM {table} WHERE {col} = @value ORDER BY id";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        resultList.Add(dr.GetDateTime(0));
                    }

                    dr.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public byte[] GetAtributeByteArray(string atribute, string table, string col, int value)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdIdAudiogram = $"SELECT {atribute} FROM {table} WHERE {col} = @value";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdIdAudiogram, connection);
                    cmd.Parameters.AddWithValue("value", value);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    byte[] attributeByteArray = null;

                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            long dataSize = dr.GetBytes(0, 0, null, 0, 0);
                            attributeByteArray = new byte[dataSize];
                            dr.GetBytes(0, 0, attributeByteArray, 0, (int)dataSize);
                        }
                    }

                    dr.Close();
                    return attributeByteArray;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        #endregion General

        #region HealthCenter

        public void AddHealthCenter(List<HealthCenterModel> dataModels)
        {
            try
            {
                foreach (var datas in dataModels)
                {
                    if (!_context.HealthCenters.Any(u => u.Id == datas.Id))
                    {
                        _context.HealthCenters.Add(datas);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void AddHealthCenters()
        {
            var context = new WaveFitContext();

            foreach (var datas in HealthCenterTable.HealthCenters())
            {
                if (!context.HealthCenters.Any(u => u.Nickname == datas.Nickname))
                {
                    context.HealthCenters.Add(datas);
                }
            }
            context.SaveChanges();
        }

        public void AddAudiometer(List<AudiometerModel> dataModels)
        {
            try
            {
                foreach (var datas in dataModels)
                {
                    if (!_context.Audiometers.Any(u => u.Id == datas.Id))
                    {
                        _context.Audiometers.Add(datas);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void AddAudiometers()
        {
            var context = new WaveFitContext();

            foreach (var datas in AudiometerTable.Audiometer())
            {
                if (!context.Audiometers.Any(u => u.Code == datas.Code))
                {
                    context.Audiometers.Add(datas);
                }
            }
            context.SaveChanges();
        }

        public void AddPlace(List<PlaceModel> dataModels)
        {
            try
            {
                foreach (var datas in dataModels)
                {
                    if (!_context.Places.Any(u => u.Id == datas.Id))
                    {
                        _context.Places.Add(datas);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void AddPlaces()
        {
            var context = new WaveFitContext();

            foreach (var datas in PlaceTable.Places())
            {
                if (!context.Places.Any(u => u.Address == datas.Address))
                {
                    context.Places.Add(datas);
                }
            }
            context.SaveChanges();
        }

        public void AddLocation(List<LocationModel> locationModels)
        {
            try
            {
                foreach (var datas in locationModels)
                {
                    if (!_context.Locations.Any(u => u.Id == datas.Id))
                    {
                        _context.Locations.Add(datas);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void AddLocations()
        {
            var context = new WaveFitContext();

            foreach (var datas in LocationTable.Locations())
            {
                if (!context.Locations.Any(u => u.State == datas.State))
                {
                    context.Locations.Add(datas);
                }
            }
            context.SaveChanges();
        }

        public List<HealthCenterModel> GetHealthCenter(int code)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string consultation = $"SELECT name, logo, cnpj, telephone, idplace, idaudiometer " +
                                          $"FROM dbo.healthcenter " +
                                          $"WHERE id=@CODE";
                    NpgsqlCommand cmd = new NpgsqlCommand(consultation, connection);
                    cmd.Parameters.AddWithValue("@CODE", code);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    List<HealthCenterModel> prints = new List<HealthCenterModel>();

                    while (dr.Read())
                    {
                        string logoBytes;

                        if (!dr.IsDBNull(1))
                        {
                            long len = dr.GetBytes(1, 0, null, 0, 0);
                            byte[] buffer = new byte[len];
                            dr.GetBytes(1, 0, buffer, 0, (int)len);
                            logoBytes = Convert.ToBase64String(buffer);
                        }
                        else
                        {
                            logoBytes = null;
                        }

                        HealthCenterModel print = new HealthCenterModel
                        {
                            Name = dr.GetString(0),
                            Logo = Convert.FromBase64String(logoBytes),
                            CNPJ = dr.GetString(2),
                            Telephone = dr.GetString(3),
                            IdPlace = dr.GetInt32(4),
                            IdAudiometer = dr.GetInt32(5),
                        };
                        prints.Add(print);
                    }
                    dr.Close();

                    return prints;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return null; // Retorne null em caso de erro ou se nenhum paciente for encontrado
        }

        public List<HealthCenterModel> LoadHealthCenterDataFromDatabase(bool status)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdSelect = $"SELECT id, logo, nickname, cnpj, telephone, idaudiometer, idplace, name FROM dbo.healthcenter WHERE status = @Status ORDER BY id";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdSelect, connection);
                    cmd.Parameters.AddWithValue("Status", status);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    List<HealthCenterModel> datas = new List<HealthCenterModel>();

                    while (dr.Read())
                    {
                        string logoBytes;

                        if (!dr.IsDBNull(1))
                        {
                            long len = dr.GetBytes(1, 0, null, 0, 0);
                            byte[] buffer = new byte[len];
                            dr.GetBytes(1, 0, buffer, 0, (int)len);
                            logoBytes = Convert.ToBase64String(buffer);
                        }
                        else
                        {
                            logoBytes = null;
                        }

                        HealthCenterModel data = new HealthCenterModel
                        {
                            Id = dr.GetInt32(0),
                            Logo = Convert.FromBase64String(logoBytes),
                            Nickname = dr.GetString(2),
                            CNPJ = dr.GetString(3),
                            Telephone = dr.GetString(4),
                            IdAudiometer = dr.GetInt32(5),
                            IdPlace = dr.GetInt32(6),
                            Name = dr.GetString(7)
                        };

                        datas.Add(data);
                    }

                    dr.Close();

                    return datas;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<AudiometerModel> LoadAudiometerDataFromDatabase(int id)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdSelect = $"SELECT code, model, maintenance " +
                                       $"FROM dbo.audiometer " +
                                       $"WHERE id = @Id";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdSelect, connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    List<AudiometerModel> datas = new List<AudiometerModel>();

                    while (dr.Read())
                    {
                        AudiometerModel data = new AudiometerModel
                        {
                            Code = dr.GetString(0),
                            Model = dr.GetString(1),
                            Maintenance = dr.GetDateTime(2).Date,
                        };

                        datas.Add(data);
                    }

                    dr.Close();

                    return datas;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<LocationModel> LoadLocationDataFromDatabase(int id)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdSelect = $"SELECT acronym, state FROM dbo.location WHERE id = @Id";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdSelect, connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    List<LocationModel> datas = new List<LocationModel>();

                    while (dr.Read())
                    {
                        LocationModel data = new LocationModel
                        {
                            Acronym = dr.GetString(0),
                            State = dr.GetString(1)
                        };

                        datas.Add(data);
                    }

                    dr.Close();

                    return datas;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<PlaceModel> LoadPlaceDataFromDatabase(int id)
        {
            var connection = new NpgsqlConnection(Properties.Settings.Default.connectDB);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string cmdSelect = $"SELECT idlocation, city, area, address, cep FROM dbo.place WHERE id = @Id";
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdSelect, connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    List<PlaceModel> datas = new List<PlaceModel>();

                    while (dr.Read())
                    {
                        PlaceModel data = new PlaceModel
                        {
                            IdLocation = dr.GetInt32(0),
                            City = dr.GetString(1),
                            Area = dr.GetString(2),
                            Address = dr.GetString(3),
                            CEP = dr.GetString(4)
                        };

                        datas.Add(data);
                    }

                    dr.Close();

                    return datas;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion HealthCenter
    }
}