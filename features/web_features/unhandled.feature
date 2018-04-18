Feature: Web unhandled errors

Background:
  When I set environment variable "MAZE_API_KEY" to "a35a2a72bd230ac0aa0f52715bbdc6aa"
  And I start the bugsnag endpoint

Scenario Outline:
  Given I have cleared the bugsnag requests
  And the <service> has been started
  And I wait for the app to respond
  When I navigate to the route "/unhandled"
  Then Bugsnag receives an error payload

  Examples:
  | service      |
  | "aspnetcore" |
