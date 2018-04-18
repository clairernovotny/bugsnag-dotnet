# Any 'run once' setup should go here as this file is evaluated
# when the environment loads.
# Any helper functions added here will be available in step
# definitions

# Scenario hooks
Before do
  find_default_docker_compose
end

After do
# Runs after every Scenario
end

at_exit do
  stop_stack
end
