import build, package, upload
import argparse
from os.path import basename
import subprocess

projects = {
    "TypeD" : {
        "projects" : ["Editor/TypeD", "Editor/TypeDitor", "TypeOCore"],
        "external" : ["Ookii.Dialogs.Wpf.dll"],
        "module"   : False
    },
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
    "TypeDCore" : {
        "projects" : ["Editor/TypeDCore"],
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
        "external" : ["SDL2/release"],
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
    parser.add_argument('-st', '--skip_tag',           type=bool, default=False,     help="Skip git tag")
    parser.add_argument('-k',  '--key',                type=str,  required=True,     help="Space key")
    parser.add_argument('-s',  '--secret',             type=str,  required=True,     help="Space secret")
    parser.add_argument('-d',  '--deploy_path_prefix', type=str,  default="",        help="deploy path configuration prefix")
    parser.add_argument('-t',  '--tag_commit_hash',    type=str,  default="develop", help="Git commit hash for tag")
    
    args = parser.parse_args()

    project = projects[args.project]

    if not args.skip_building:
        for subproject in project["projects"]:
            build.build(subproject, args.build_number, args.config, args.output)

    if not args.skip_packing:
        subprojects = project["projects"]
        dependencies = sum([projects[subproject]["external"] for subproject in subprojects if subproject in projects], project["external"])

        print(dependencies)

        package_project = package.pack(args.project, subprojects, args.build_number, dependencies, args.output)

        if not args.skip_uploading:
            upload_package(args.key, args.secret, package_project, "typeo/releases{}{}/{}".format(args.deploy_path_prefix, ("/modules" if project["module"] else ""), args.project))

    if not args.skip_tag:
        version = getVersion(args.project)
        print("Tagging git commit with tag {}".format(version))

        try:
            subprocess.run(["git", "tag", "-a", "-f", "-m", version, version], check=True)
            subprocess.run(["git", "push", "origin", "-f", version], check=True)
        except subprocess.CalledProcessError as e:
            print(e.output)
            raise e

def upload_package(key, secret, package, dir):
    path = "{}/{}".format(dir, basename(package))

    upload.upload(key, secret, "typedeaf", path, package)

def getVersion(project):
    path = "../{0}/version".format(project)

    with open(path) as f:
        return "{}-{}".format(f.readline().replace("\n", "").replace("\r", ""), project)

if __name__ == "__main__":
    main()