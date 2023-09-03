ignore := 'obj|bin|docs|test*'
basePath := $(PWD)
jsonPath := $(basePath)/data/
authorizerPath := $(basePath)/src/
bin := $(authorizerPath)/bin/Release/net6.0/authorizer

# Usage samples:
# 
#   make build 
#   make publish
#   make clean
#   make all

build:
		@echo "\nBuilding solution..."
		@cd $(authorizerPath) && dotnet build --configuration Release

publish:
		@echo "\nPublishing solution..."
		@cd $(authorizerPath) && dotnet publish --configuration Release

clean:
		@echo "\nCleaning solution..."
		@cd $(authorizerPath) && rm -rf ./bin ./obj
		@echo "Cleaning done"

tree:
		@tree $(basePath) -I $(ignore)

all: clean build publish tree
