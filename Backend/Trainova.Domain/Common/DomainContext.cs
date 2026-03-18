namespace Trainova.Domain.Common
{
///<summary>
    /// *** Exercises and fitness training ***
    /// Exercises should be in our system as a entity in a one to many relations with the Training sessions(trining TrainingSession).
///</summary>

///<summary>
    /// *** Plans and TrainingSessions ***
        /// Plans are made due to a specific goal.
        /// Plans can contain many TrainingSessions in.
        /// TrainingSessions Can Be Plannless (not assigned to a plan).
        /// 
    /// <Canciled>
        /// *** Plans and TrainingSessions ***
        /// Plans are made due to a specific goal.
        /// Plans can contain many TrainingSessions in and there may many plannable TrainingSessions but not assigned to a plan.
        /// TrainingSessions can be plannable(can be assigned to a plan ) or unplannable (can't be assigned to a plan ).
    /// </Canciled>
///</summary>

///<summary>
    ///*** Medical history and injures and examination ***
    /// players medical history has to be spectated due to the need of player health tracking.
    /// players examination data has to be considered as medical history.
    /// players injures should be maintained and analyses for until the full recovery.
///</summary>



    public class DomainMarker
    {
    }
}
