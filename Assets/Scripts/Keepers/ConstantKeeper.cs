
namespace ConstantKeeper
{
	public static class Debugs
	{
		public static readonly string IsCanceled = " iptal edildi!";
		public static readonly string IsFaulted = " başarısız oldu!";
		public static readonly string IsCompleted = " başarıyla tamamlandı!";
	}

	public static class Authentications
	{
		public static readonly string SignIn = "Oturum açma işlemi";
		public static readonly string SignUp = "Kayıt olma işlemi";
		public static readonly string SignOut = "Oturum kapatma işlemi";
		public static readonly string ResetPassword = "Şifre sıfırlama bağlantısı gönderme";
		public static readonly string DeleteUser = "Kullanıcı silme işlemi";

	}

	public static class UserTasks
	{
		public static readonly string GetCurrentUserProfile = "Kullanıcı verileri çekme işlemi";
		public static readonly string UpdateCurrentUserProfile = "Kullanıcı verileri güncelleme işlemi";
		public static readonly string DeleteCurrentUserProfile = "Kullanıcı verileri silme işlemi";
	}

	public static class UserPaths
	{
		// Main Paths
		public static readonly string Users = "Users";
		public static readonly string UserID = "UserID";


		// Secondary Paths
		public static readonly string General = "General";
		public static readonly string Progression = "Progression";
		public static readonly string Consumable = "Consumable";


		// String General Paths
		public static readonly string Username = "Username";
		public static readonly string Country = "Country";
		public static readonly string Language = "Language";
		public static readonly string SignUpDate = "SignUpDate";
		public static readonly string LastSeen = "LastSeen";

		// Bool General Paths
		public static readonly string SignInStatus = "SignInStatus";
		public static readonly string Intermateable = "Intermateable";

		// Int Progression Paths
		public static readonly string Level = "Level";
		public static readonly string Cup = "Cup";
		public static readonly string Rank = "Rank";
		public static readonly string TotalPlayTime = "TotalPlayTime";
		public static readonly string TotalMatches = "TotalMatches";
		public static readonly string CompletedMatches = "CompletedMatches";
		public static readonly string AbandonedMatches = "AbandonedMatches";
		public static readonly string Wins = "Wins";
		public static readonly string Losses = "Losses";
		public static readonly string WinningStreak = "WinningStreak";

		// Int Consumable Paths
		public static readonly string Gem = "Gem";
		public static readonly string Papcoin = "Papcoin";
		public static readonly string Energy = "Energy";
	}

	public static class GameSettingsPaths
	{
		// Main Paths
		public static readonly string GameSettings = "GameSettings";
		
		// Secondary Paths
		public static readonly string Localization = "Localization";
		public static readonly string SoundSettings = "SoundSettings";

		// Localization
		public static readonly string AbandonedMatches = "AbandonedMatches";
		public static readonly string Cancel = "Cancel";
		public static readonly string CompletedMatches = "CompletedMatches";
		public static readonly string Cup = "Cup";
		public static readonly string Gem = "Gem";
		public static readonly string Energy = "Energy";
		public static readonly string Estimate = "Estimate";
		public static readonly string Papcoin = "Papcoin";
		public static readonly string LastSeen = "LastSeen";
		public static readonly string Level = "Level";
		public static readonly string Loading = "Loading";
		public static readonly string Lose = "Lose";
		public static readonly string Losses = "Losses";
		public static readonly string Music = "Music";
		public static readonly string Ok = "Ok";
		public static readonly string Online = "Online";
		public static readonly string Password = "Password";
		public static readonly string Play = "Play";
		public static readonly string QuickGame = "QuickGame";
		public static readonly string Rank = "Rank";
		public static readonly string RateUs = "RateUs";
		public static readonly string Rival = "Rival";
		public static readonly string Score = "Score";
		public static readonly string SecretNumber = "SecretNumber";
		public static readonly string Settings = "Settings";
		public static readonly string SignIn = "SignIn";
		public static readonly string SignOut = "SignOut";
		public static readonly string SignUp = "SignUp";
		public static readonly string Store = "Store";
		public static readonly string TotalTimePlayed = "TotalTimePlayed";
		public static readonly string TotalMatches = "TotalMatches";
		public static readonly string UserProfile = "UserProfile";
		public static readonly string Username = "Username";
		public static readonly string Vibration = "Vibration" ;
		public static readonly string Volume = "Volume";
		public static readonly string Win = "Win";
		public static readonly string WinningStreak = "WinningStreak";
		public static readonly string Wins = "Wins";
		public static readonly string You = "You";



		
	}

	public static class RoomPaths 
	{
		// Main Paths
		public static readonly string Rooms = "Rooms";

		// Secondary Paths
		public static readonly string RoomID = "RoomID";
		public static readonly string General = "General";
		public static readonly string Progression = "Progression";
		
		// General Paths
		public static readonly string P1_ID = "P1_ID";
		public static readonly string P1_Username = "P1_Username";
		public static readonly string P2_ID = "P2_ID";
		public static readonly string P2_Username = "P2_Username";
		public static readonly string ScoreLimit = "ScoreLimit";
		public static readonly string AnswerTimeLimit = "AnswerTimeLimit";
		public static readonly string PlayerLimit = "PlayerLimit";
		public static readonly string SecretNumber = "SecretNumber";
		public static readonly string SecretNumberMaxValue = "SecretNumberMaxValue";
		public static readonly string Penetrability = "Penetrability";

		// Progression Paths
		public static readonly string LastEstimation = "LastEstimation";
		public static readonly string WhoseTurn = "WhoseTurn";
	}
}
