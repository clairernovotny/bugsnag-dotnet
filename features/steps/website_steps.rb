require "pathname"
require "httparty"
require "retryable"
require "test/unit"

When("I start the bugsnag endpoint") do
  steps %Q{
    When I set environment variable "MAZE_ENDPOINT" to "http://bugsnag"
    And the "bugsnag" service has been started
  }
end

Given("I have cleared the bugsnag requests") do
  Services.bugsnag.clear_requests
end

Given("the {string} service has been started") do |service|
  start_service service
  Retryable.retryable(tries: 10, sleep: 6) do
    Services.send(service).health_check
  end
end

When("I cause an {string} exception on {string}") do |exception_type, service|
  Services.send(service).send(exception_type)
end

Then("Bugsnag receives an error payload") do
  assert_equal 1, Bugsnag.requests.size
end
