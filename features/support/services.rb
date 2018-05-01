require "httparty"
require "logger"

class Services
  def self.method_missing(name)
    name = name.to_sym
    frameworks[name] ||= begin
      case name
      when :bugsnag
        Service.new(name).extend(Bugsnag)
      else
        Service.new(name).extend(TestService)
      end
    end

    frameworks[name]
  end

  def self.frameworks
    @frameworks ||= {}
  end

  class Service
    include HTTParty
    logger ::Logger.new STDOUT

    def initialize(service_name)
      self.class.base_uri(container_ip_address_for(service_name))
    end
  end

  module TestService
    def health_check
      self.class.get("/")
    end

    def unhandled
      self.class.get("/unhandled")
    end
  end

  module Bugsnag
    def health_check
      self.class.get("/health_check")
    end

    def requests
      self.class.get("/requests").parsed_response
    end

    def clear_requests
      self.class.delete("/requests")
    end
  end
end
