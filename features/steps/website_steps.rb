require "pathname"
require "httparty"
require "retryable"
require "test/unit"

When("I start the bugsnag endpoint") do
  steps %Q{
    When I set environment variable "MAZE_ENDPOINT" to "http://bugsnag"
    And I start the service "bugsnag"
  }
end

Given("I have cleared the bugsnag requests") do
  Bugsnag.clear_requests
end

Given("the {string} has been started") do |service|
  start_service service
end

When("I wait for the app to respond") do
  Retryable.retryable(tries: :infinite) do
    HTTParty.get("http://localhost:8081")
  end
end

When("I navigate to the route {string}") do |path|
  steps %Q{
    When I open the URL "http://localhost:8081#{path}"
    And I wait for 5 seconds
  }
end

Then("Bugsnag receives an error payload") do
  assert_equal 1, Bugsnag.requests.size
end
