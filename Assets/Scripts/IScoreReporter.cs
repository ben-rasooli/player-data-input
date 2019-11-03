namespace PlayerDataInput
{
   /// <summary>
   /// Any object in the game who is responsible for keeping track of scores
   /// should implements this interfce and then be passed to the Form object.
   /// </summary>
   public interface IScoreReporter
   {
      Score Report();
   }
}
