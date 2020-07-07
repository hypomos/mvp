ARG TAG="3.1"

FROM mcr.microsoft.com/dotnet/core/sdk:${TAG}

RUN curl -sL https://deb.nodesource.com/setup_12.x | bash - \
    && apt-get install -y --no-install-recommends nodejs \
    && echo "node version: $(node --version)" \
    && echo "npm version: $(npm --version)" \
    && rm -rf /var/lib/apt/lists/*