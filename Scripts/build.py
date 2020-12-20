import argparse, sys, os
 
def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-p', '--project', type=str, required=True, help="Project name to build")
    parser.add_argument('-c', '--config', type=str, default="Debug", help="Debug|Release defaults to Debug")
    parser.add_argument('-o', '--output', type=str, help="Output folder")
    args = parser.parse_args()

    build(args.project, args.config, args.output)

def build(project, config="Debug", output=None):
    print("Building '{}' with config '{}' to output '{}'".format(project, config, output))

    path = "../{0}/{0}.csproj".format(project)

    if(output == None or output == ""):
        output = "../bin/builds/{}/{}".format(project, config)

    os.system("dotnet clean {} --configuration {} --output {} --verbosity n".format(path, config, output))
    os.system("dotnet publish {} --configuration {} --output {} --verbosity n".format(path, config, output))

if __name__ == "__main__":
    main()