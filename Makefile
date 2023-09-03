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
#   make test
#   make run json=operations.json

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

test:
		@clear && make run json=operations.json
		@clear && make run json=operations-account-not-initialized.json
		@clear && make run json=operations-account-already-initialized.json
		@clear && make -i run json=operations-card-not-active.json
		@clear && make -i run json=operations-doubled-transaction.json
		@clear && make -i run json=operations-high-frequency-small-interval.json
		@clear && make -i run json=operations-insufficient-limit.json
		@clear && make -i run json=operations-multiple-rules-breaks.json
		@clear && make -i run json=operations-empty.json

run:
		@echo "\njson:" $(json)
		@cat $(jsonPath)$(json) | jq .
		@echo "\nresult:\n"
		@cat $(jsonPath)$(json) | $(bin)
		@echo -e '\nPress enter to continue'
		@read ans
