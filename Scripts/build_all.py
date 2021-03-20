import build, package, upload
import argparse
from os.path import basename

projects = {
    "TypeO" : {
        "projects" : ["TypeOCore", "TypeOBasic2d", "TypeODesktop", "TypeOSDL"],
        "external" : [],
        "module"   : False
    },
    "TypeOCore" : {
        "projects" : ["TypeOCore"],
        "external" : [],
        "module"   : True
    },
    "TypeOBasic2d" : {
        "projects" : ["TypeOBasic2d"],
        "external" : [],
        "module"   : True
    },
    "TypeODesktop" : {
        "projects" : ["TypeODesktop"],
        "external" : [],
        "module"   : True
    },
    "TypeOSDL" : {
        "projects" : ["TypeOSDL"],
        "external" : ["SDL2"],
        "module"   : True
    },
}

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-b',  '--build_number',       type=int,  required=True,     help="Build number to append to versioning")
    parser.add_argument('-o',  '--output',             type=str,  default="..",      help="Output folder")
    parser.add_argument('-p',  '--project',            type=str,  required=True,     help="Project to build")
    parser.add_argument('-c',  '--config',             type=str,  default="Release", help="Debug|Release defaults to Release")
    parser.add_argument('-sb', '--skip_building',      type=bool, default=False,     help="Skip building")
    parser.add_argument('-sp', '--skip_packing',       type=bool, default=False,     help="Skip packing")
    parser.add_argument('-su', '--skip_uploading',     type=bool, default=False,     help="Skip uploading")
    parser.add_argument('-k',  '--key',                type=str,  required=True,     help="Space key")
    parser.add_argument('-s',  '--secret',             type=str,  required=True,     help="Space secret")
    parser.add_argument('-d',  '--deploy_path_prefix', type=str,  default="",        help="deploy path configuration prefix")
    args = parser.parse_args()

    project = projects[args.project]

    if not args.skip_building:
        for subproject in project["projects"]:
            build.build(subproject, args.build_number, args.config, args.output)

    if not args.skip_packing:
        subprojects = project["projects"]
        dependencies = sum([projects[subproject]["external"] for subproject in subprojects], [])

        package_project = package.pack(args.project, subprojects, args.build_number, dependencies, args.output)

        if not args.skip_uploading:
            upload_package(args.key, args.secret, package_project, "typeo/releases{}{}/{}".format(args.deploy_path_prefix, ("/modules" if project["module"] else ""), args.project))

    return getVersion(args.project)

def upload_package(key, secret, package, dir):
    path = "{}/{}".format(dir, basename(package))

    upload.upload(key, secret, "typedeaf", path, package)

def getVersion(project):
    path = "../{0}/version".format(project)

    with open(path) as f:
        return "{}".format(f.readline().replace("\n", "").replace("\r", "").replace("v", ""))

if __name__ == "__main__":
    main()