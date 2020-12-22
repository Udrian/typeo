import build, package
import argparse

class Module(object):
    def __init__(self, name, external=[]): 
        self.name = name
        self.external = external

configs = ["Debug", "Release"]
modules = [
    Module("TypeOCore"),
    Module("TypeODesktop"),
    Module("TypeOSDL", ["sdl2"])
]

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-b', '--build', type=int, required=True, help="Build number to append to versioning")
    parser.add_argument('-o', '--output', type=str, default="..", help="Output folder")
    parser.add_argument('-s', '--skip', type=bool, default=False, help="Skip building")
    args = parser.parse_args()

    if not args.skip:
        build_all(args.build, args.output)
    package_module(args.build, args.output)
    package_typeo(args.build, args.output)

def build_all(build_number, output=".."):
    for module in modules:
        for config in configs:
            build.build(module.name, build_number, config, output)

def package_module(build_number, output=".."):
    for module in modules:
        package.pack(module.name, [module.name], build_number, module.external, output)

def package_typeo(build_number, output=".."):
    projects = [module.name for module in modules]
    dependencies = sum([module.external for module in modules if module.external], [])

    package.pack("TypeO", projects, build_number, dependencies, output)

if __name__ == "__main__":
    main()