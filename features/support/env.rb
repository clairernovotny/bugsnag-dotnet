# Any 'run once' setup should go here as this file is evaluated
# when the environment loads.
# Any helper functions added here will be available in step
# definitions

##
#
# On certain versions of docker for windows we cannot address the container via
# localhost, so we need to determine the ip address of the container to connect
# to it
#
def container_ip_address_for(container_name)
  container_id = run_docker_compose_command($docker_compose_file, "ps -q #{container_name}").first
  command = [
    "docker",
    "inspect",
    "-f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}'",
    container_id
  ]
  IO.popen(command) do |io|
    io.read.tr("'\n ", "")
  end
end

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
