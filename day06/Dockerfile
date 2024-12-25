FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /aoc
RUN git clone https://github.com/89netraM/Advent-of-Code.git .
WORKDIR /aoc/2024
RUN dotnet build -c Release
RUN ln /bin/true /bin/xsel
WORKDIR /aoc/2024/6
ENTRYPOINT "/bin/pwsh" "-c" "dotnet run --project .. --no-build -c Release -- 6 \$(if (\$Env:part -eq 'part1') {'1'} elseif (\$Env:part -eq 'part2') {'2'} else {'--benchmark'})"
