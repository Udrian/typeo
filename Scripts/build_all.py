import build
import argparse, sys, os

configs = ["Debug", "Release"]
modules = ["TypeOCore", "TypeODesktop", "TypeOSDL"]

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-b', '--build', type=int, required=True, help="Build number to append to versioning")
    parser.add_argument('-o', '--output', type=str, help="Output folder")
    args = parser.parse_args()

    build_modules(args.build, args.output)

def build_modules(build_number, output=None):
    for module in modules:
        for config in configs:
            build.build(module, build_number, config, output)

if __name__ == "__main__":
    main()