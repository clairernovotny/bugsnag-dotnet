function analyseCodeAnalysisResults( [Parameter(ValueFromPipeline=$true)]$codeAnalysisResultsFile ) {
  $codeAnalysisErrors = [xml](Get-Content $codeAnalysisResultsFile);

  foreach ($codeAnalysisError in $codeAnalysisErrors.SelectNodes("//Message")) {
    $issueNode = $codeAnalysisError.SelectSingleNode("Issue");
    Add-AppveyorTest "Violation of Rule $($codeAnalysisError.CheckId): $($codeAnalysisError.TypeName) Line Number: $($issueNode.Line)" -Outcome Failed -FileName "$($issueNode.Path)\$($codeAnalysisError.Issue.File)" -ErrorMessage $($issueNode.InnerXml);
  }
  
  Push-AppveyorArtifact $codeAnalysisResultsFile;
}

function analyseStyleCopResults( [Parameter(ValueFromPipeline=$true)]$styleCopResultsFile ) {
  $styleCopViolations = [xml](Get-Content $styleCopResultsFile)
  
  foreach ($styleCopViolation in $styleCopViolations.StyleCopViolations.Violation) {
    Add-AppveyorTest "Violation of Rule $($styleCopViolation.RuleId): $($styleCopViolation.Rule) Line Number: $($styleCopViolation.LineNumber)" -Outcome Warning -FileName $styleCopViolation.Source -ErrorMessage $styleCopViolation.InnerXml
  }

  Push-AppveyorArtifact $styleCopResultsFile;
}
