import build

configs = ["Debug", "Release"]
modules = ["TypeOCore", "TypeODesktop", "TypeOSDL"]

def main():
    build_modules()

def build_modules():
    for module in modules:
        for config in configs:
            build.build(module, config)

if __name__ == "__main__":
    main()