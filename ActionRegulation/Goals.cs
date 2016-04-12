using System.Collections.Generic;

namespace ActionRegulation
{
    class Goals
    {
        List<string> preConditions, successConditions, failureConditions, driveEffects;

        public Goals(List<string> preConditions, List<string> successConditions, List<string> failureConditions, List<string> driveEffects)
        {
            this.preConditions = preConditions;
            this.successConditions = successConditions;
            this.failureConditions = failureConditions;
            this.driveEffects = driveEffects;
        }
    }
}
