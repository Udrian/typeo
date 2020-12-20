import build, package
import argparse

configs = ["Debug", "Release"]
modules = ["TypeOCore", "TypeODesktop", "TypeOSDL"]

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-b', '--build', type=int, required=True, help="Build number to append to versioning")
    parser.add_argument('-o', '--output', type=str, default="..", help="Output folder")
    parser.add_argument('-s', '--skip', type=bool, default=False, help="Skip building")
    args = parser.parse_args()

    build_modules(args.build, args.output, args.skip)

def build_modules(build_number, output="..", skip_build=False):
    if not skip_build:
        for module in modules:
            for config in configs:
                build.build(module, build_number, config, output)

    for module in modules:
        package.pack(module, build_number, output)

if __name__ == "__main__":
    main()