use std::fs::File;
use std::io::{self, BufRead};
use std::path::Path;


fn main() {
    let file_path="./src/input.txt" ;

    println!("In file {}", file_path);

 // File hosts.txt must exist in the current path
 if let Ok(lines) = read_lines(file_path) {
    // Consumes the iterator, returns an (Optional) String
    for line in lines {
        if let Ok(ip) = line {
            println!("{}", ip);
        }
    }
}

}

fn read_lines<P>(filename: P) -> io::Result<io::Lines<io::BufReader<File>>>
where P: AsRef<Path>, {
    let file = File::open(filename)?;
    Ok(io::BufReader::new(file).lines())
}