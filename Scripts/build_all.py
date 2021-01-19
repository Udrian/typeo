import build, package, upload
import argparse
from os.path import basename

class Module(object):
    def __init__(self, name, external=[]): 
        self.name = name
        self.external = external

configs = ["Debug", "Release"]
modules = [
    Module("TypeOCore"),
    Module("TypeODesktop"),
    Module("TypeOSDL", ["SDL2"])
]

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-b', '--build', type=int, required=True, help="Build number to append to versioning")
    parser.add_argument('-o', '--output', type=str, default="..", help="Output folder")
    parser.add_argument('-sb', '--skip_building', type=bool, default=False, help="Skip building")
    parser.add_argument('-sp', '--skip_packing', type=bool, default=False, help="Skip packing")
    parser.add_argument('-su', '--skip_uploading', type=bool, default=False, help="Skip uploading")
    parser.add_argument('-k', '--key', type=str, required=True, help="Space key")
    parser.add_argument('-s', '--secret', type=str, required=True, help="Space secret")
    args = parser.parse_args()

    if not args.skip_building:
        build_all(args.build, args.output)
    if not args.skip_packing:
        package_modules = pack_modules(args.build, args.output)
        package_typeo = pack_typeo(args.build, args.output) 

        if not args.skip_uploading:
            upload_modules(args.key, args.secret, package_modules)
            upload_package(args.key, args.secret, package_typeo, "typeo/releases/TypeO")

def build_all(build_number, output=".."):
    for module in modules:
        for config in configs:
            build.build(module.name, build_number, config, output)

def pack_modules(build_number, output=".."):
    packages = []
    for module in modules:
        packages += [package.pack(module.name, [module.name], build_number, module.external, output)]
    return packages

def pack_typeo(build_number, output=".."):
    projects = [module.name for module in modules]
    dependencies = sum([module.external for module in modules if module.external], [])

    return package.pack("TypeO", projects, build_number, dependencies, output)

def upload_modules(key, secret, packages):
    for package in packages:
        module = basename(package).split('-')[0]
        upload_package(key, secret, package, "typeo/releases/modules/{}".format(module))

def upload_package(key, secret, package, dir):
    path = "{}/{}".format(dir, basename(package))

    upload.upload(key, secret, "typedeaf", path, package)

if __name__ == "__main__":
    main()