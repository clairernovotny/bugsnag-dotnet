require "httparty"

class Bugsnag
  include HTTParty

  def self.configure_base_uri
    base_uri ip_address
  end

  def self.ip_address
    container_ip_address_for "bugsnag"
  end

  def self.requests
    configure_base_uri
    get("/requests").parsed_response
  end

  def self.clear_requests
    configure_base_uri
    delete("/requests")
  end
end
