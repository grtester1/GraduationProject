using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.Models
{
    public class ProgressReportModel
    {
        public float PercentageComplete { get; private set; } = 0;
        public List<string> CompletedLoadingStages { get; set; } = new List<string>();
        private int progressResolution;

        public ProgressReportModel(int i_ProgressResolution)
        {
            progressResolution = i_ProgressResolution;
        }

        public void Progress()
        {
            if (PercentageComplete >= 100)
                throw new Exception("Progress Bar is overflown");
            PercentageComplete += (float)1/progressResolution;
        }

        public ProgressReportModel AddStage(string i_StageCompleted)
        {
            CompletedLoadingStages.Add(i_StageCompleted);

            return this;
        }
    }
}
