# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Install Node.js (required for Playwright)
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y nodejs

# Install the Playwright CLI
RUN npm install -g playwright

# Copy everything to the container
COPY . /app
WORKDIR /app

# Set environment variables to bypass SSL issues
ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0

# Restore and build the project (commented out for debugging)
# RUN dotnet restore
# RUN dotnet build --configuration Release --output /app/build

# Install Playwright browsers (commented out for debugging)
# RUN playwright install

# Use runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build-env /app .

# Entry point for debugging
ENTRYPOINT ["/bin/bash"]